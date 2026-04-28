namespace Chip8.Core;


//TIMERS - FX07, FX15, FX18
partial class Chip8CPU {

    private void Op_FX07(Byte Vx) {
        Registers[Vx] = DelayTimer;
    }

    private void Op_FX15(Byte Vx) {
        DelayTimer = Registers[Vx];
    }

    private void Op_FX18(Byte Vx) {
        SoundTimer = Registers[Vx];
    }

}
