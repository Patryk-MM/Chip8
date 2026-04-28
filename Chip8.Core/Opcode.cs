namespace Chip8.Core {
    public readonly struct Opcode (UInt16 value) {
        public UInt16 Raw => value;
        public Byte Category => (Byte)((value >> 12) & 0xF);
        public Byte Vx => (Byte)((value >> 8) & 0xF);
        public Byte Vy => (Byte)((value >> 4) & 0xF);
        public Byte N => (Byte)(value & 0xF);
        public Byte NN => (Byte)(value & 0xFF);
        public UInt16 NNN => (UInt16)(value & 0xFFF);
    }
}
