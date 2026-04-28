# C# CHIP-8 Emulator

A custom, fully functional [CHIP-8](https://en.wikipedia.org/wiki/CHIP-8) virtual machine and emulator written in C#. It features an accurate execution loop, display rendering, and input handling powered by the Raylib framework.

## Features
* **Accurate CPU Loop:** Emulates the standard CHIP-8 clock speed (~700Hz).
* **Precise Timers:** Delay and sound timers running accurately at 60Hz.
* **Raylib Integration:** Hardware-accelerated rendering, keyboard input, and sound generation using `Raylib-cs`.
* **Dynamic ROM Loading:** Easily load any `.ch8` ROM file via command-line arguments.
* **Logging System:** Built-in CPU instruction and state logging using `Microsoft.Extensions.Logging` for easy debugging.

## Tech Stack
* **Language:** C# 12 / .NET 8
* **Graphics/Audio:** [Raylib-cs](https://github.com/NotNotBatman/Raylib-cs) (C# bindings for Raylib)

## Getting Started

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Installation and Execution

1. **Clone the repository:**
    git clone https://github.com/Patryk-MM/Chip8.git
    cd Chip8

2. **Navigate to the core project folder:**
    cd Chip8.Core

3. **Run the emulator with a ROM:**
    Pass the path to your CHIP-8 ROM as a command-line argument.
    
    dotnet run -- path/to/your/rom.ch8
    
    *Note: If no arguments are passed, the emulator attempts to load a default `ibm_logo.ch8` file in the execution directory.*

## Controls
*The CHIP-8 uses a standard 16-key hexadecimal keypad (0-F). Check the `Chip8.KeyMap.cs` file in the source code to see how the original layout maps to your modern QWERTY keyboard.*
