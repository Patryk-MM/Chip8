using Raylib_cs;

namespace Chip8.Core;

//INPUT HANDLING - FX0A
partial class Chip8CPU {

    private void Op_EX9E(byte vx) {
        byte targetKey = Registers[vx];
        KeyboardKey physicalKey = KeyMap[targetKey];

        if (Raylib.IsKeyDown(physicalKey)) {
            ProgramCounter += 2;
        }
    }
    private void Op_EXA1(byte vx) {
        byte targetKey = Registers[vx];
        KeyboardKey physicalKey = KeyMap[targetKey];

        if (!Raylib.IsKeyDown(physicalKey)) { // Notice the '!' for NOT down
            ProgramCounter += 2;
        }
    }

    private void Op_FX0A(Byte Vx) {
        IsWaitingForKeyPress = true;

        _keyTargetRegister = Vx;
    }
}
