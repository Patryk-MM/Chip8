using Microsoft.Extensions.Logging;
using Raylib_cs;

namespace Chip8.Core;

// https://tobiasvl.github.io/blog/write-a-chip-8-emulator/ - tutorial
// https://mintlify.wiki/Ferryistaken/CHIP8-rs/concepts/instruction-set - instruction categories
public partial class Chip8CPU {
    private readonly ILogger _logger;

    private Byte[] Memory = new Byte[4096]; //Size of 4096 in real emulator
    Byte[] Registers = new Byte[16]; //8-bit, V0 - VF
    UInt16 IndexRegister = new UInt16();

    Stack<UInt16> Stack = new();

    UInt16 ProgramCounter = 0x0200; //0x0 for testing, 0x0200 in real emulator

    Byte DelayTimer = 60;
    Byte SoundTimer = 60;
    Sound beep;

    Byte _keyTargetRegister; //Helper field for handling input

    internal bool[,] Display = new bool[32, 64];
    private byte _pixelSize;

    Opcode Opcode = new Opcode();

    private readonly Action[] _jumpTable = new Action[16];
    private readonly Action[] _table0 = new Action[256];
    private readonly Action[] _table8 = new Action[16];
    private readonly Action[] _tableE = new Action[256];
    private readonly Action[] _tableF = new Action[256];

    internal bool IsWaitingForKeyPress { get; set; } = false;  
    public Chip8CPU (ILogger logger, byte pixelSize) {
        _logger = logger;

        _pixelSize = pixelSize;


        Raylib.InitWindow(64 * pixelSize, 32 * pixelSize, "CHIP-8");

        Raylib.InitAudioDevice();
        beep = Raylib.LoadSound("beep.wav");


        //Initialize the tables
        Array.Fill(_jumpTable, Op_Unimplemented);
        Array.Fill(_table0, Op_Unimplemented);
        Array.Fill(_table8, Op_Unimplemented);
        Array.Fill(_tableE, Op_Unimplemented);
        Array.Fill(_tableF, Op_Unimplemented);

        _table0[0xE0] = () => Op_0E00();
        _table0[0xEE] = () => Op_00EE();

        _table8[0x0] = () => Op_8XY0(Opcode.Vx, Opcode.Vy);
        _table8[0x1] = () => Op_8XY1(Opcode.Vx, Opcode.Vy);
        _table8[0x2] = () => Op_8XY2(Opcode.Vx, Opcode.Vy);
        _table8[0x3] = () => Op_8XY3(Opcode.Vx, Opcode.Vy);
        _table8[0x4] = () => Op_8XY4(Opcode.Vx, Opcode.Vy);
        _table8[0x5] = () => Op_8XY5(Opcode.Vx, Opcode.Vy);
        _table8[0x6] = () => Op_8XY6(Opcode.Vx, Opcode.Vy);
        _table8[0x7] = () => Op_8XY7(Opcode.Vx, Opcode.Vy);
        _table8[0xE] = () => Op_8XYE(Opcode.Vx, Opcode.Vy);

        _tableE[0x9E] = () => Op_EX9E(Opcode.Vx);
        _tableE[0xA1] = () => Op_EXA1(Opcode.Vx);

        _tableF[0x07] = () => Op_FX07(Opcode.Vx);
        _tableF[0x0A] = () => Op_FX0A(Opcode.Vx);
        _tableF[0x15] = () => Op_FX15(Opcode.Vx);
        _tableF[0x18] = () => Op_FX18(Opcode.Vx);
        _tableF[0x1E] = () => Op_FX1E(Opcode.Vx);
        _tableF[0x29] = () => Op_FX29(Opcode.Vx);
        _tableF[0x33] = () => Op_FX33(Opcode.Vx);
        _tableF[0x55] = () => Op_FX55(Opcode.Vx);
        _tableF[0x65] = () => Op_FX65(Opcode.Vx);


        _jumpTable[0x0] = () => _table0[Opcode.NN]();
        _jumpTable[0x1] = () => Op_1NNN(Opcode.NNN);
        _jumpTable[0x2] = () => Op_2NNN(Opcode.NNN);
        _jumpTable[0x3] = () => Op_3XNN(Opcode.Vx, Opcode.NN);
        _jumpTable[0x4] = () => Op_4XNN(Opcode.Vx, Opcode.NN);
        _jumpTable[0x5] = () => Op_5XY0(Opcode.Vx, Opcode.Vy);
        _jumpTable[0x6] = () => Op_6XNN(Opcode.Vx, Opcode.NN);
        _jumpTable[0x7] = () => Op_7XNN(Opcode.Vx, Opcode.NN);
        _jumpTable[0x8] = () => _table8[Opcode.N]();
        _jumpTable[0x9] = () => Op_9XY0(Opcode.Vx, Opcode.Vy);
        _jumpTable[0xB] = () => Op_BXNN(Opcode.Vx, Opcode.NN);
        _jumpTable[0xA] = () => Op_ANNN(Opcode.NNN);
        _jumpTable[0xC] = () => Op_CXNN(Opcode.Vx, Opcode.NN);
        _jumpTable[0xD] = () => Op_DXYN(Opcode.Vx, Opcode.Vy, Opcode.N);
        _jumpTable[0xE] = () => _tableE[Opcode.NN]();
        _jumpTable[0xF] = () => _tableF[Opcode.NN]();
        _logger.LogInformation("CPU Initialized");
    }

    public void LoadFonts(byte[] data) {
        UInt16 start = 0x0;

        for (int i = 0; i < data.Length; i++) {
            Memory[start + i] = data[i];
        }

        _logger.LogInformation("Fonts loaded to memory.");
    }

    public void LoadMemory(byte[] data) {
        UInt16 start = 0x0200;

        for (int i = 0; i < data.Length; i++) {
            Memory[start + i] = data[i];
        }

        _logger.LogInformation("ROM loaded to memory.");
    }

    public void LoadMemory(string filePath) {
        byte[] data = File.ReadAllBytes(filePath);
        LoadMemory(data);
    }

    public void TickTimers() {
        if (DelayTimer > 0) DelayTimer--;
        if (SoundTimer > 0) SoundTimer--;
    }

    public void FetchAndDecode() {
        Opcode = new Opcode((UInt16)((Memory[ProgramCounter] << 8) | Memory[ProgramCounter + 1]));
        ProgramCounter += 2;
    }

    public void Execute() {
        _jumpTable[Opcode.Category]();
    }

    public void HandleInput() {
        KeyboardKey key = (KeyboardKey)Raylib.GetKeyPressed();
        int mappedKey = MapKey(key);

        if (mappedKey != -1) {
            Registers[_keyTargetRegister] = (Byte)mappedKey;
            IsWaitingForKeyPress = false;
        }
    }

    public void PlaySound() {
        if(SoundTimer > 0) {
            if (!Raylib.IsSoundPlaying(beep)) Raylib.PlaySound(beep);
        }
        else if(SoundTimer == 0) {
            if (Raylib.IsSoundPlaying(beep)) Raylib.StopSound(beep);
        }
    }

    private void Op_Unimplemented() {
        throw new NotImplementedException($"{Opcode.Raw:X4}");
    }

    public void RenderDisplay() {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        for (int y = 0; y < 32; y++) {
            for (int x = 0; x < 64; x++) {
                if (Display[y,x]) {
                    Raylib.DrawRectangle(x * _pixelSize, y * _pixelSize, _pixelSize, _pixelSize, Color.White);

                }
            }
        }
        Raylib.EndDrawing();
    }
}
