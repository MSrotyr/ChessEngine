using ChessEngine.Utils;

namespace ChessEngine.SearchUtils
{
    public static class Rook
    {
        // Useful bitboards
        public const ulong WhiteQueenSideHome = 0x0000000000000001UL; // A1
        public const ulong WhiteKingSideHome  = 0x0000000000000080UL; // H1
        public const ulong BlackQueenSideHome = 0x0100000000000000UL; // A8
        public const ulong BlackKingSideHome  = 0x8000000000000000UL; // H8
        public const ulong WhiteCastleKingSide = 0x0000000000000020UL; // F1
        public const ulong WhiteCastleQueenSide = 0x0000000000000008UL; // D1
        public const ulong BlackCastleKingSide = 0x2000000000000000UL; // F8
        public const ulong BlackCastleQueenSide = 0x0800000000000000UL; // D8


        // Magic


        public static void Initialize()
        {
            for (var i = 0; i < 64; i++)
            {
                var bb = 1UL << i;

                ulong blockerMask = 
                    GetMovesInDirection(bb, CommonBitBoards.AllTiles, 8) |  // N
                    GetMovesInDirection(bb, CommonBitBoards.NotHFile, 1) |  // E
                    GetMovesInDirection(bb, CommonBitBoards.AllTiles, -8) | // S
                    GetMovesInDirection(bb, CommonBitBoards.NotAFile, -1);  // W

                // Remove edges
                if ((bb & CommonBitBoards.Rank1) == 0)
                    blockerMask &= ~CommonBitBoards.Rank1;

                if ((bb & CommonBitBoards.Rank8) == 0)
                    blockerMask &= ~CommonBitBoards.Rank8;

                if ((bb & ~CommonBitBoards.NotHFile) == 0)
                    blockerMask &= CommonBitBoards.NotHFile;

                if ((bb & ~CommonBitBoards.NotAFile) == 0)
                    blockerMask &= CommonBitBoards.NotAFile;

                var blockerBoards = GenBlockerBoards(blockerMask);
            }
        }

        private static List<ulong> GenBlockerBoards(ulong blockerMask)
        {
            List<ulong> bits = [];

            while(blockerMask != 0)
            {
                bits.Add(blockerMask.GetLsb());
                blockerMask = blockerMask.PopLsb();
            }

            List<ulong> blockerBoards = [0UL];
            foreach(var bit in bits)
            {
                int existingCnt = blockerBoards.Count;

                for (var i = 0; i < existingCnt; i++)
                {
                    blockerBoards.Add(blockerBoards[i] | bit);
                }
            }

            return blockerBoards;
        }

        private static ulong GetMovesInDirection(ulong bb, ulong notBarrierFile, int direction)
        {
            var shift1 = Shift(bb & notBarrierFile, direction);
            var shift2 = Shift(shift1 & notBarrierFile, direction);
            var shift3 = Shift(shift2 & notBarrierFile, direction);
            var shift4 = Shift(shift3 & notBarrierFile, direction);
            var shift5 = Shift(shift4 & notBarrierFile, direction);
            var shift6 = Shift(shift5 & notBarrierFile, direction);
            var shift7 = Shift(shift6 & notBarrierFile, direction);

            return shift1 | shift2 | shift3 | shift4 | shift5 | shift6 | shift7;
        }

        private static ulong Shift(ulong b, int direction)
        {
            return direction > 0 ? b << direction : b >> Math.Abs(direction);
        }
    }
}