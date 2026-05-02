using System.Runtime.Intrinsics.X86;

namespace ChessEngine.Utils
{
    public static class BitOperations
    {
        public static ulong PopLsb(this ulong value)
        {
            return Bmi1.X64.ResetLowestSetBit(value);
        }

        public static ulong GetLsb(this ulong value)
        {
            return Bmi1.X64.ExtractLowestSetBit(value);
        }

        public static ulong GetTrailingZeroCount(this ulong value)
        {
            return Bmi1.X64.TrailingZeroCount(value);
        }
    }
}