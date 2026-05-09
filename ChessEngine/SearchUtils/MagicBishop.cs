using System.Runtime.CompilerServices;
using ChessEngine.Utils;

namespace ChessEngine.SearchUtils
{
    public static class MagicBishop
    {
        private static ulong[] moveBoards = [];
        private static readonly Magic[] magic = new Magic[64];
        public static Magic[] Initialize(ulong[]? magicNumbers = null)
        {
            for (var i = 0; i < 64; i++)
            {
                var blockerMoveBoardMap = new Dictionary<ulong, ulong>();

                var bb = 1UL << i;

                ulong blockerMask = 
                    Search.GetMovesInDirection(bb, CommonBitBoards.AllTiles, CommonBitBoards.AllTiles, 9) |  // NE
                    Search.GetMovesInDirection(bb, CommonBitBoards.AllTiles, CommonBitBoards.NotHFile, -7) |  // SE
                    Search.GetMovesInDirection(bb, CommonBitBoards.AllTiles, CommonBitBoards.AllTiles, -9) | // SW
                    Search.GetMovesInDirection(bb, CommonBitBoards.AllTiles, CommonBitBoards.NotAFile, 7);  // NW

                // Remove edges
                if ((bb & CommonBitBoards.Rank1) == 0)
                    blockerMask &= ~CommonBitBoards.Rank1;

                if ((bb & CommonBitBoards.Rank8) == 0)
                    blockerMask &= ~CommonBitBoards.Rank8;

                if ((bb & ~CommonBitBoards.NotHFile) == 0)
                    blockerMask &= CommonBitBoards.NotHFile;

                if ((bb & ~CommonBitBoards.NotAFile) == 0)
                    blockerMask &= CommonBitBoards.NotAFile;

                var blockers = MagicNumbers.GenBlockerBoards(blockerMask);

                foreach(var blocker in blockers)
                {
                    var moves = Search.GenBishopMoves(bb, CommonBitBoards.AllTiles, blocker);
                    blockerMoveBoardMap.Add(blocker, moves);
                }

                int offset = moveBoards.Length;
                ulong? num = magicNumbers?[i];
                (ulong magicNum, ulong[] mov) = MagicNumbers.GenMagicMoveArr(blockerMask, blockerMoveBoardMap, num);

                var newArr = new ulong[offset + mov.Length];
                // Copy old array
                for (var j = 0; j < moveBoards.Length; j++)
                {
                    newArr[j] = moveBoards[j];
                }

                for (var j = 0; j < mov.Length; j++)
                {
                    newArr[offset + j] = mov[j];
                }

                moveBoards = newArr;
                magic[i] = new Magic
                {
                  BlockerMask = blockerMask,
                  MagicNumber = magicNum,
                  Shift = 64 - blockerMask.GetNumSetBits(),
                  Offset = offset
                };
            }

            return magic;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMoves(int squareIndex, ulong occupied)
        {
            ref readonly var m = ref magic[squareIndex];
            occupied &= m.BlockerMask;
            ulong index = (occupied * m.MagicNumber) >> m.Shift;
            return moveBoards[m.Offset + (int)index];
        }
    }
}