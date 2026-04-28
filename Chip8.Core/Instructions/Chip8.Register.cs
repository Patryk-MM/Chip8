namespace Chip8.Core;


//REGISTER OPERATIONS - 6XNN, 7XNN
public partial class Chip8CPU {
    

    /// <summary>
    /// Set register: sets the value of register <paramref name="Vx"/> to <paramref name="NN"/>.
    /// </summary>
    /// <param name="Vx">Register to update.</param>
    /// <param name="NN">Value to put in the register.</param>
    private void Op_6XNN(Byte Vx, Byte NN) => Registers[Vx] = NN;


    /// <summary>
    /// Add to register: add the value of <paramref name="NN"/> to <paramref name="Vx"/>.
    /// </summary>
    /// <param name="Vx">Register to update.</param>
    /// <param name="NN">Value to put in the register.</param>
    private void Op_7XNN(Byte Vx, Byte NN) => Registers[Vx] += NN;



}

