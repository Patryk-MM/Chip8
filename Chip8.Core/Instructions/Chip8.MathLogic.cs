namespace Chip8.Core;


//MATH AND LOGIC - 8XY0, 8XY1, 8XY2, 8XY3, 8XY4, 8XY5, 8XY6, 8XY7, 8XYE
public partial class Chip8CPU {

    /// <summary>
    /// Set register: sets <paramref name="Vx"/> to the value of <paramref name="Vy"/>.
    /// </summary>
    /// <param name="Vx">Register <paramref name="Vx"/></param>
    /// <param name="Vy">Register <paramref name="Vy"/></param>
    private void Op_8XY0(Byte Vx, Byte Vy) => Registers[Vx] = Registers[Vy];


    /// <summary>
    /// Binary OR: sets <paramref name="Vx"/> to the bitwise/binary logical disjunction (OR) of <paramref name="Vx"/> and <paramref name="Vy"/>. 
    /// <paramref name="Vy"/> is not affected.
    /// </summary>
    /// <param name="Vx">Register <paramref name="Vx"/></param>
    /// <param name="Vy">Register <paramref name="Vy"/></param>
    private void Op_8XY1(Byte Vx, Byte Vy) => Registers[Vx] |= Registers[Vy];


    /// <summary>
    /// Binary AND: sets <paramref name="Vx"/> to the bitwise/binary logical conjunction (AND) of <paramref name="Vx"/> and <paramref name="Vy"/>. 
    /// <paramref name="Vy"/> is not affected.
    /// </summary>
    /// <param name="Vx">Register <paramref name="Vx"/></param>
    /// <param name="Vy">Register <paramref name="Vy"/></param>
    private void Op_8XY2(Byte Vx, Byte Vy) => Registers[Vx] &= Registers[Vy];


    /// <summary>
    /// Binary XOR: sets <paramref name="Vx"/> to the bitwise/binary exclusive OR (XOR) of <paramref name="Vx"/> and <paramref name="Vy"/>. 
    /// <paramref name="Vy"/> is not affected.
    /// </summary>
    /// <param name="Vx">Register <paramref name="Vx"/></param>
    /// <param name="Vy">Register <paramref name="Vy"/></param>
    private void Op_8XY3(Byte Vx, Byte Vy) => Registers[Vx] ^= Registers[Vy];


    /// <summary>
    /// Add with overflow: sets <paramref name="Vx"/> to the result of <paramref name="Vx"/> + <paramref name="Vy"/>. 
    /// Sets the VF overflow flag to 1 if the result is over 255.
    /// </summary>
    /// <param name="Vx">Register <paramref name="Vx"/></param>
    /// <param name="Vy">Register <paramref name="Vy"/></param>
    private void Op_8XY4(Byte Vx, Byte Vy) {
        Registers[0xF] = (Registers[Vx] + Registers[Vy] > 255) ? (Byte)1 : (Byte)0;
        Registers[Vx] += Registers[Vy];
    }

    /// <summary>
    /// Subtraction: sets <paramref name="Vx"/> to the result of <paramref name="Vx"/> - <paramref name="Vy"/>. 
    /// Sets the VF underflow flag to 0 if the second operand is larger.
    /// </summary>
    /// <param name="Vx">Register <paramref name="Vx"/></param>
    /// <param name="Vy">Register <paramref name="Vy"/></param>
    private void Op_8XY5(Byte Vx, Byte Vy) {
        Registers[0xF] = Registers[Vx] >= Registers[Vy] ? (Byte)1 : (Byte)0;

        Registers[Vx] -= Registers[Vy];
    }

    /// <summary>
    /// Shift right: shift the value of <paramref name="Vx"/> one bit to the right.
    /// Sets the VF flag based on shifted out bit.
    /// </summary>
    /// <param name="Vx">Register <paramref name="Vx"/></param>
    /// <param name="Vy">Register <paramref name="Vy"/></param>
    private void Op_8XY6(Byte Vx, Byte Vy) {
        Registers[0xF] = (Byte)(Registers[Vx] & 0x1);
        Registers[Vx] >>= 1;
    }

    /// <summary>
    /// Subtraction: sets <paramref name="Vx"/> to the result of <paramref name="Vy"/> - <paramref name="Vx"/>. 
    /// Sets the VF underflow flag to 0 if the second operand is larger.
    /// </summary>
    /// <param name="Vx">Register <paramref name="Vx"/></param>
    /// <param name="Vy">Register <paramref name="Vy"/></param>
    private void Op_8XY7(Byte Vx, Byte Vy) {
        Registers[0xF] = Registers[Vy] >= Registers[Vx] ? (Byte)1 : (Byte)0;

        Registers[Vx] = (byte)(Registers[Vy] - Registers[Vx]);
    }

    /// <summary>
    /// Shift left: shift the value of <paramref name="Vx"/> one bit to the left.
    /// Sets the VF flag based on shifted out bit.
    /// </summary>
    /// <param name="Vx">Register <paramref name="Vx"/></param>
    /// <param name="Vy">Register <paramref name="Vy"/></param>
    private void Op_8XYE(Byte Vx, Byte Vy) {
        Registers[0xF] = (Byte)((Registers[Vx] & 0x80) >> 7);
        Registers[Vx] <<= 1;
    }
}

