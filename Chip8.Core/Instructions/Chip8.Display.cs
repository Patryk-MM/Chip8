namespace Chip8.Core;


//DISPLAY INSTRUCTIONS - 0E00, DXYN
public partial class Chip8CPU {
    /// <summary>
    /// Clear display: sets all the pixels in the display to 0.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void Op_0E00() => Display = new bool[32, 64];
    private void Op_DXYN(Byte Vx, Byte Vy, Byte N) {
        int x = Registers[Vx] % 64;
        int y = Registers[Vy] % 32;

        Registers[0xF] = 0x0;

        for (int row = 0; row < N; row++) {
            if (y + row >= 32) break;

            byte sprite = Memory[IndexRegister + row];

            for (int bit = 0; bit < 8; bit++) {
                if (x + bit >= 64) break;

                if (((sprite >> (7 - bit)) & 0x1) == 0x1) {

                    if (Display[y + row, x + bit]) {
                        Registers[0xF] = 0x1;
                    }

                    Display[y + row, x + bit] ^= true;
                }
            }
        }
    }
}

