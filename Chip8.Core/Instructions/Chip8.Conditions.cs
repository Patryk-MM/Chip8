namespace Chip8.Core;


// SKIP CONDITIONALLY - 3XNN, 4XNN, 5XY0, 9XY0
partial class Chip8CPU {
    private void Op_3XNN(Byte Vx, Byte NN) {
        if (Registers[Vx] == NN) ProgramCounter += 2;
    }

    private void Op_4XNN(Byte Vx, Byte NN) {
        if (Registers[Vx] != NN) ProgramCounter += 2;
    }

    private void Op_5XY0(Byte Vx, Byte Vy) {
        if (Registers[Vx] == Registers[Vy]) ProgramCounter += 2; 
    }

    private void Op_9XY0(Byte Vx, Byte Vy) {
        if (Registers[Vx] != Registers[Vy]) ProgramCounter += 2;
    }

}