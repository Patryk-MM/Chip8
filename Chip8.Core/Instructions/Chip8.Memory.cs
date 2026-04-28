namespace Chip8.Core;


//MEMORY OPERATIONS - ANNN, FX55, FX65
partial class Chip8CPU {

    private void Op_ANNN(UInt16 NNN) => IndexRegister = NNN;


    private void Op_FX1E(Byte Vx) {
        IndexRegister += Registers[Vx];
        Registers[0xF] = (IndexRegister > 0xFFF) ? (Byte)1 : (Byte)0;
    }


    private void Op_FX29(Byte Vx) {
        IndexRegister = (Byte)(Registers[Vx] * 5);
    }

    private void Op_FX55(Byte Vx) {
        for (int i = 0; i <= Vx; i++) {
            Memory[IndexRegister + i] = Registers[i];
        }
    }
    private void Op_FX65(Byte Vx) {
        for (int i = 0; i <= Vx; i++) {
             Registers[i] = Memory[IndexRegister + i];
        }
    }
}
