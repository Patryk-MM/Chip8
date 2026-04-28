using Raylib_cs;

namespace Chip8.Core;
partial class Chip8CPU {
    internal int MapKey(KeyboardKey key) {
        return key switch {
            KeyboardKey.X       => 0x0,
            KeyboardKey.One     => 0x1,
            KeyboardKey.Two     => 0x2,
            KeyboardKey.Three   => 0x3,
            KeyboardKey.Q       => 0x4,
            KeyboardKey.W       => 0x5,
            KeyboardKey.E       => 0x6,
            KeyboardKey.A       => 0x7,
            KeyboardKey.S       => 0x8,
            KeyboardKey.D       => 0x9,
            KeyboardKey.Z       => 0xA,
            KeyboardKey.C       => 0xB,
            KeyboardKey.Four    => 0xC,
            KeyboardKey.R       => 0xD,
            KeyboardKey.F       => 0xE,
            KeyboardKey.V       => 0xF,
            _                   => -1
        };
    }

    // The index is the CHIP-8 key (0-15), the value is the Raylib key
    internal readonly KeyboardKey[] KeyMap = new KeyboardKey[16] {
    KeyboardKey.X,    // 0
    KeyboardKey.One,  // 1
    KeyboardKey.Two,  // 2
    KeyboardKey.Three,// 3
    KeyboardKey.Q,    // 4
    KeyboardKey.W,    // 5
    KeyboardKey.E,    // 6
    KeyboardKey.A,    // 7
    KeyboardKey.S,    // 8
    KeyboardKey.D,    // 9
    KeyboardKey.Z,    // A
    KeyboardKey.C,    // B
    KeyboardKey.Four, // C
    KeyboardKey.R,    // D
    KeyboardKey.F,    // E
    KeyboardKey.V     // F
};
}
