namespace ChessEngine.Utils
{
    public static class RandomUtils
    {
        public static ulong NextULong(this Random random)
        {
            var buffer = new byte[8]; // 8 bytes = 64 bits
            random.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}