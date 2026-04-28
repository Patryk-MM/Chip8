namespace Chip8.Core;


//SPECIAL OPERATIONS - CXNN, FX33
partial class Chip8CPU {

    private void Op_CXNN(Byte Vx, Byte NN) {
        Random random = new Random();

        Byte rand = (Byte)random.Next(256);
        Registers[Vx] = (Byte)(rand & NN);
    }


    private void Op_FX33(Byte Vx) {
        int value = Registers[Vx];

        Memory[IndexRegister] = (Byte)(value / 100);
        Memory[IndexRegister + 1] = (Byte)((value / 10) % 10);
        Memory[IndexRegister + 2] = (Byte)(value % 10);
    }
}
