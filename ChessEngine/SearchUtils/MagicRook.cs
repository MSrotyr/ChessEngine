using System.Runtime.CompilerServices;
using ChessEngine.Utils;

namespace ChessEngine.SearchUtils
{
    public struct Magic
    {
        public ulong BlockerMask;
        public ulong MagicNumber;
        public int Shift;
        public int Offset;
    }

    public static class MagicRook
    {
        private static readonly Random random = new Random();
        private static ulong[] moveBoards = [];
        private static readonly Magic[] magic = new Magic[64];
        public static Magic[] Initialize(ulong[]? magicNumbers = null)
        {
            for (var i = 0; i < 64; i++)
            {
                var blockerMoveBoardMap = new Dictionary<ulong, ulong>();

                var bb = 1UL << i;

                ulong blockerMask = 
                    Search.GetMovesInDirection(bb, CommonBitBoards.AllTiles, CommonBitBoards.AllTiles, 8) |  // N
                    Search.GetMovesInDirection(bb, CommonBitBoards.AllTiles, CommonBitBoards.NotHFile, 1) |  // E
                    Search.GetMovesInDirection(bb, CommonBitBoards.AllTiles, CommonBitBoards.AllTiles, -8) | // S
                    Search.GetMovesInDirection(bb, CommonBitBoards.AllTiles, CommonBitBoards.NotAFile, -1);  // W

                // Remove edges
                if ((bb & CommonBitBoards.Rank1) == 0)
                    blockerMask &= ~CommonBitBoards.Rank1;

                if ((bb & CommonBitBoards.Rank8) == 0)
                    blockerMask &= ~CommonBitBoards.Rank8;

                if ((bb & ~CommonBitBoards.NotHFile) == 0)
                    blockerMask &= CommonBitBoards.NotHFile;

                if ((bb & ~CommonBitBoards.NotAFile) == 0)
                    blockerMask &= CommonBitBoards.NotAFile;

                var blockers = GenBlockerBoards(blockerMask);

                foreach(var blocker in blockers)
                {
                    var moves = Search.GenRookMoves(bb, CommonBitBoards.AllTiles, blocker);
                    blockerMoveBoardMap.Add(blocker, moves);
                }

                int offset = moveBoards.Length;
                ulong? num = magicNumbers?[i];
                (ulong magicNum, ulong[] mov) = GenMagicMoveArr(blockerMask, blockerMoveBoardMap, num);

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
        public static ulong GetRookMoves(int squareIndex, ulong occupied)
        {
            ref readonly var m = ref magic[squareIndex];
            occupied &= m.BlockerMask;
            ulong index = (occupied * m.MagicNumber) >> m.Shift;
            return moveBoards[m.Offset + (int)index];
        }

        private static (ulong MagicNumber, ulong[] MoveBoards) GenMagicMoveArr(ulong blockerMask, Dictionary<ulong, ulong> blockerMoveBoardMap, ulong? num = null)
        {
            ulong magicNum = num == null 
                ? random.NextULong() & random.NextULong() & random.NextULong() // Magic numbers tend to be sparse
                : num.Value;

            while(true)
            {
                bool isMagic = true;

                int bits = blockerMask.GetNumSetBits();
                var moveBoards = new ulong[(int)Math.Pow(2, bits)];

                // Since zero is a valid move board we first set it to a board no piece could have
                // So we know if it has been set or not
                Array.Fill(moveBoards, CommonBitBoards.AllTiles);

                foreach(var kvp in blockerMoveBoardMap)
                {
                    var blocker = kvp.Key;
                    var moves = kvp.Value;

                    var index = (blocker * magicNum) >> (64 - bits);
                    
                    // Unset => set for this index
                    if (moveBoards[index] == CommonBitBoards.AllTiles)
                    {
                        moveBoards[index] = moves;
                    } 
                    
                    // If already set for a different blocker board BUT the move boards match then that is okay
                    // Both blockers will map to this index
                    else if (moveBoards[index] == moves)
                    {
                        continue;
                    }

                    // 2 blocker boards with different move boards point to the same index
                    // therefore the random number is not magic and we must try another
                    else
                    {
                        isMagic = false;
                        magicNum = random.NextULong() & random.NextULong() & random.NextULong();
                        break;
                    }
                }

                if (isMagic)
                {
                    return (magicNum, moveBoards);
                }
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
    }
}