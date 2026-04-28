namespace Chip8.Core;


//FLOW CONTROL - 1NNN, 2NNN, 00EE, BXNN
public partial class Chip8CPU {
    /// <summary>
    /// Jump: sets the program counter to the value of <paramref name="NNN"/>.
    /// </summary>
    /// <param name="NNN">Jump location</param>
    /// <exception cref="NotImplementedException"></exception>
    private void Op_1NNN(UInt16 NNN) => ProgramCounter = NNN;

    private void Op_2NNN(UInt16 NNN) {
        Stack.Push(ProgramCounter);
        ProgramCounter = NNN;
    }

    private void Op_00EE() => ProgramCounter = Stack.Pop();

    private void Op_BXNN(Byte Vx, Byte NN) => ProgramCounter = (UInt16)(NN + Registers[Vx]);
}

