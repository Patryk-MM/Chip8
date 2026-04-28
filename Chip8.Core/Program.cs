using Chip8.Core;
using Microsoft.Extensions.Logging;
using Raylib_cs;
using System.Diagnostics;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder
.AddConsole()
.AddDebug()
.SetMinimumLevel(LogLevel.Debug));

ILogger logger = factory.CreateLogger("CHIP-8");

byte[] fonts = {
    0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
    0x20, 0x60, 0x20, 0x20, 0x70, // 1
    0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
    0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
    0x90, 0x90, 0xF0, 0x10, 0x10, // 4
    0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
    0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
    0xF0, 0x10, 0x20, 0x40, 0x40, // 7
    0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
    0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
    0xF0, 0x90, 0xF0, 0x90, 0x90, // A
    0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
    0xF0, 0x80, 0x80, 0x80, 0xF0, // C
    0xE0, 0x90, 0x90, 0x90, 0xE0, // D
    0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
    0xF0, 0x80, 0xF0, 0x80, 0x80  // F
};

const int Hz = 700;
const int FPS = 60;
const int pixelSize = 25;

string romPath = "ibm_logo.ch8";

Chip8CPU cpu = new Chip8CPU(logger, pixelSize);


double accCpuTime = 0;
double accTimers = 0;

double lastTime = 0;

cpu.LoadFonts(fonts);

if (args.Length > 0) {
    romPath = args[0];
}

cpu.LoadMemory(args[0]);

Stopwatch sw = Stopwatch.StartNew();

while (!Raylib.WindowShouldClose()) {
    double elapsed = sw.Elapsed.TotalMilliseconds;
    double delta = elapsed - lastTime;
    lastTime = elapsed;

    accCpuTime += delta;
    accTimers += delta;

    while (accCpuTime > (1000.0 / Hz)) {
        if (!cpu.IsWaitingForKeyPress) {
            cpu.FetchAndDecode();
            cpu.Execute();
        }
        else {
            cpu.HandleInput();
        }
        accCpuTime -= 1000.0 / Hz;
    }

    while (accTimers > (1000.0 / FPS)) {
        cpu.TickTimers();
        cpu.PlaySound();

        accTimers -= 1000.0 / FPS;
    }

   
    cpu.RenderDisplay();

}
