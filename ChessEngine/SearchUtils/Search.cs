using ChessEngine.Utils;
using Player;

namespace ChessEngine.SearchUtils
{
    public static class Search
    {
        // King Move Shifts
        const int N = 8;
        const int S = -8;
        const int E = 1;
        const int W = -1;
        const int NE = 9;
        const int NW = 7;
        const int SE = -7;
        const int SW = -9;

        // Knight shifts
        const int KnNNE = 17;
        const int KnENE = 10;
        const int KnESE = -6;
        const int KnSSE = -15;
        const int KnSSW = -17;
        const int KnWSW = -10;
        const int KnWNW = 6;
        const int KnNNW = 15;

        // Attack masks
        static readonly ulong[] knightMoves = new ulong[64];
        static ulong[] basicKingMoves = new ulong[64]; // Doesn't include castling

        // Initiate
        static Search()
        {
            for (var i = 0; i < 64; i++)
            {
                knightMoves[i] = GenAllKnightMoves(1ul << i);
                basicKingMoves[i] = GenAllBasicKingMoves(1ul << i);
            }
        }

        public static int GetPossibleMoves(Board board, Move[] moves)
        {
            Move[] movesInternal = new Move[256];

            var player = board.GetCurrentPlayer();
            ulong empty = ~board.CurrentBoard.Occupied;
            ulong attacked = GetAttackedBitBoard(board.CurrentBoard, player); // TODO only need this if can castle

            int intMoveIndex = 0;
            if (player == PlayerEnum.White)
            {
                ulong emptyOrEnemy = ~board.CurrentBoard.WhiteOccupied;
                ulong enemyOrEnPassant = board.CurrentBoard.BlackOccupied | board.CurrentBoard.EnPassant;
                intMoveIndex = GetWhitePawnMoves(board.CurrentBoard.WhitePawns, empty, enemyOrEnPassant, movesInternal, intMoveIndex);
                intMoveIndex = GetKnightMoves(board.CurrentBoard.WhiteKnights, emptyOrEnemy, movesInternal, intMoveIndex);
                intMoveIndex = GetBishopMoves(board.CurrentBoard.WhiteBishops, emptyOrEnemy, board.CurrentBoard.Occupied, movesInternal, intMoveIndex);
                intMoveIndex = GetRookMoves(board.CurrentBoard.WhiteRooks, emptyOrEnemy, board.CurrentBoard.Occupied, movesInternal, intMoveIndex);
                intMoveIndex = GetQueenMoves(board.CurrentBoard.WhiteQueens, emptyOrEnemy, board.CurrentBoard.Occupied, movesInternal, intMoveIndex);
                intMoveIndex = GetKingMoves(
                    board.CurrentBoard.WhiteKing,
                    emptyOrEnemy,
                    attacked,
                    board.CurrentBoard.Occupied,
                    board.CurrentBoard.HasWhiteKingSideCastlingRights,
                    board.CurrentBoard.HasWhiteQueenSideCastlingRights,
                    player,
                    movesInternal,
                    intMoveIndex);
            } else
            {
                ulong emptyOrEnemy = ~board.CurrentBoard.BlackOccupied;
                ulong enemyOrEnPassant = board.CurrentBoard.WhiteOccupied | board.CurrentBoard.EnPassant;
                intMoveIndex = GetBlackPawnMoves(board.CurrentBoard.BlackPawns, empty, enemyOrEnPassant, movesInternal, intMoveIndex);
                intMoveIndex = GetKnightMoves(board.CurrentBoard.BlackKnights, emptyOrEnemy, movesInternal, intMoveIndex);
                intMoveIndex = GetBishopMoves(board.CurrentBoard.BlackBishops, emptyOrEnemy, board.CurrentBoard.Occupied, movesInternal, intMoveIndex);
                intMoveIndex = GetRookMoves(board.CurrentBoard.BlackRooks, emptyOrEnemy, board.CurrentBoard.Occupied, movesInternal, intMoveIndex);
                intMoveIndex = GetQueenMoves(board.CurrentBoard.BlackQueens, emptyOrEnemy, board.CurrentBoard.Occupied, movesInternal, intMoveIndex);
                intMoveIndex = GetKingMoves(
                    board.CurrentBoard.BlackKing,
                    emptyOrEnemy,
                    attacked,
                    board.CurrentBoard.Occupied,
                    board.CurrentBoard.HasBlackKingSideCastlingRights,
                    board.CurrentBoard.HasBlackQueenSideCastlingRights,
                    player,
                    movesInternal,
                    intMoveIndex);
            }

            // Check validation
            int moveIndex = 0;
            for (var i = 0; i < intMoveIndex; i++)
            {
                board.MakeMove(movesInternal[i]);
                if (!IsPlayerInCheck(board.CurrentBoard, player))
                {
                    moves[moveIndex++] = movesInternal[i];
                }
                board.UndoLastMove();
            }

            return moveIndex;
        }

        public static int GetWhitePawnMoves(ulong bb, ulong empty, ulong enemyOrEnPassant, Move[] moves, int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                ulong pawnMoves = GenWhitePawnMoves(from, empty, enemyOrEnPassant);

                while (pawnMoves != 0)
                {
                    ulong to = pawnMoves.GetLsb();

                    if ((to & CommonBitBoards.Rank8) != 0)
                    {
                        moves[moveIndex++] = new Move(from, to, PromotionPeice.N);    
                        moves[moveIndex++] = new Move(from, to, PromotionPeice.B);    
                        moves[moveIndex++] = new Move(from, to, PromotionPeice.R);    
                        moves[moveIndex++] = new Move(from, to, PromotionPeice.Q);    
                    } else
                    {
                        moves[moveIndex++] = new Move(from, to);
                    }

                    pawnMoves = pawnMoves.PopLsb();
                }

                bb = bb.PopLsb();
            }

            return moveIndex;
        }

        private static ulong GenWhitePawnMoves(ulong bb, ulong empty, ulong enemyOrEnPassant)
        {
            ulong moves = 0;

            // North 1 if empty
            moves |= Shift(bb, N) & empty;

            // North another if on rank 3
            moves |= Shift(moves & CommonBitBoards.Rank3, N) & empty;

            // Diagonal if enemy / En Passant
            moves |= Shift(bb & CommonBitBoards.NotHFile, NE) & enemyOrEnPassant;
            moves |= Shift(bb & CommonBitBoards.NotAFile, NW) & enemyOrEnPassant;

            return moves;
        }

        private static ulong GenWhitePawnAttacks(ulong bb)
        {
            ulong moves = 0;

            // Diagonal if enemy / En Passant
            moves |= Shift(bb & CommonBitBoards.NotHFile, NE);
            moves |= Shift(bb & CommonBitBoards.NotAFile, NW);
            return moves;
        }

        private static int GetBlackPawnMoves(ulong bb, ulong empty, ulong enemyOrEnPassant, Move[] moves, int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                ulong pawnMoves = GenBlackPawnMoves(from, empty, enemyOrEnPassant);

                while (pawnMoves != 0)
                {
                    ulong to = pawnMoves.GetLsb();
                    
                    if ((to & CommonBitBoards.Rank1) != 0)
                    {
                        moves[moveIndex++] = new Move(from, to, PromotionPeice.N);    
                        moves[moveIndex++] = new Move(from, to, PromotionPeice.B);    
                        moves[moveIndex++] = new Move(from, to, PromotionPeice.R);    
                        moves[moveIndex++] = new Move(from, to, PromotionPeice.Q);    
                    } else
                    {
                        moves[moveIndex++] = new Move(from, to);
                    }

                    pawnMoves = pawnMoves.PopLsb();
                }

                bb = bb.PopLsb();
            }

            return moveIndex;
        }

        private static ulong GenBlackPawnMoves(ulong bb, ulong empty, ulong enemyOrEnPassant)
        {
            ulong moves = 0;

            // South 1 if empty
            moves |= Shift(bb, S) & empty;

            // South another if on rank 6
            moves |= Shift(moves & CommonBitBoards.Rank6, S) & empty;

            // Diagonal if enemy / En Passant
            moves |= Shift(bb & CommonBitBoards.NotHFile, SE) & enemyOrEnPassant;
            moves |= Shift(bb & CommonBitBoards.NotAFile, SW) & enemyOrEnPassant;

            return moves;
        }

        private static ulong GenBlackPawnAttacks(ulong bb)
        {
            ulong moves = 0;

            // Diagonal if enemy / En Passant
            moves |= Shift(bb & CommonBitBoards.NotHFile, SE);
            moves |= Shift(bb & CommonBitBoards.NotAFile, SW);
            return moves;
        }

        public static int GetKnightMoves(ulong bb, ulong emptyOrEnemy, Move[] moves, int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                var tileIndex = (int)from.GetTrailingZeroCount();
                ulong attacks = knightMoves[tileIndex] & emptyOrEnemy;

                while (attacks != 0)
                {
                    ulong to = attacks.GetLsb();
                    moves[moveIndex++] = new Move(from, to);
                    attacks = attacks.PopLsb();
                }

                bb = bb.PopLsb();
            }

            return moveIndex;
        }

        private static ulong GenAllKnightMoves(ulong bb)
        {
            ulong moves = 0;

            // East
            moves |= Shift(bb & CommonBitBoards.NotHFile, KnNNE);
            moves |= Shift(bb & CommonBitBoards.NotGHFile, KnENE);
            moves |= Shift(bb & CommonBitBoards.NotGHFile, KnESE);
            moves |= Shift(bb & CommonBitBoards.NotHFile, KnSSE);

            // West
            moves |= Shift(bb & CommonBitBoards.NotAFile, KnSSW);
            moves |= Shift(bb & CommonBitBoards.NotABFile, KnWSW);
            moves |= Shift(bb & CommonBitBoards.NotABFile, KnWNW);
            moves |= Shift(bb & CommonBitBoards.NotAFile, KnNNW);

            return moves;
        }

        public static int GetBishopMoves(ulong bb, ulong emptyOrEnemy, ulong occupied, Move[] moves, int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                var tileIndex = from.GetTrailingZeroCount();
                ulong bishopMoves = MagicBishop.GetMoves((int)tileIndex, occupied) & emptyOrEnemy;

                while (bishopMoves != 0)
                {
                    ulong to = bishopMoves.GetLsb();
                    moves[moveIndex++] = new Move(from, to);
                    bishopMoves = bishopMoves.PopLsb();
                }

                bb = bb.PopLsb();
            }

            return moveIndex;
        }

        public static ulong GenBishopMoves(ulong bb, ulong emptyOrEnemy, ulong enemy)
        {
            var notEnemy = ~enemy;
            return
                GetMovesInDirection(bb, emptyOrEnemy, CommonBitBoards.NotHFile & notEnemy, NE) |
                GetMovesInDirection(bb, emptyOrEnemy, CommonBitBoards.NotHFile & notEnemy, SE) |
                GetMovesInDirection(bb, emptyOrEnemy, CommonBitBoards.NotAFile & notEnemy, SW) |
                GetMovesInDirection(bb, emptyOrEnemy, CommonBitBoards.NotAFile & notEnemy, NW);
        }

        public static int GetRookMoves(ulong bb, ulong emptyOrEnemy, ulong occupied, Move[] moves, int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                var tileIndex = from.GetTrailingZeroCount();
                ulong rookMoves = MagicRook.GetMoves((int)tileIndex, occupied) & emptyOrEnemy;

                while (rookMoves != 0)
                {
                    ulong to = rookMoves.GetLsb();
                    moves[moveIndex++] = new Move(from, to);
                    rookMoves = rookMoves.PopLsb();
                }

                bb = bb.PopLsb();
            }

            return moveIndex;
        }

        public static ulong GenRookMoves(ulong bb, ulong emptyOrEnemy, ulong enemy)
        {
            var notEnemy = ~enemy;
            return
                GetMovesInDirection(bb, emptyOrEnemy, notEnemy, N) |
                GetMovesInDirection(bb, emptyOrEnemy, CommonBitBoards.NotHFile & notEnemy, E) |
                GetMovesInDirection(bb, emptyOrEnemy, notEnemy, S) |
                GetMovesInDirection(bb, emptyOrEnemy, CommonBitBoards.NotAFile & notEnemy, W);
        }

        public static int GetQueenMoves(ulong bb, ulong emptyOrEnemy, ulong occupied, Move[] moves, int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                var tileIndex = from.GetTrailingZeroCount();
                ulong bishopMoves = MagicBishop.GetMoves((int)tileIndex, occupied);
                ulong rookMoves = MagicRook.GetMoves((int)tileIndex, occupied);
                ulong queenMoves = (bishopMoves | rookMoves) & emptyOrEnemy;

                while (queenMoves != 0)
                {
                    ulong to = queenMoves.GetLsb();
                    moves[moveIndex++] = new Move(from, to);
                    queenMoves = queenMoves.PopLsb();
                }

                bb = bb.PopLsb();
            }

            return moveIndex;
        }

        private static ulong GenQueenMoves(ulong bb, ulong emptyOrEnemy, ulong enemy)
        {
            return GenBishopMoves(bb, emptyOrEnemy, enemy) | GenRookMoves(bb, emptyOrEnemy, enemy);
        }

        public static int GetKingMoves(
            ulong bb,
            ulong emptyOrEnemy,
            ulong opponentAttacked,
            ulong occupied,
            bool hasKingSideCastlingRights,
            bool hasQueenSideCastlingRights,
            PlayerEnum player,
            Move[] moves,
            int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                var tileIndex = from.GetTrailingZeroCount();
                ulong kingMoves = (basicKingMoves[tileIndex] & emptyOrEnemy) 
                | GenCastlingMoves(hasKingSideCastlingRights, hasQueenSideCastlingRights, occupied, opponentAttacked, player);

                while (kingMoves != 0)
                {
                    ulong to = kingMoves.GetLsb();
                    moves[moveIndex++] = new Move(from, to);
                    kingMoves = kingMoves.PopLsb();
                }

                bb = bb.PopLsb();
            }

            return moveIndex;
        }

        private static ulong GenCastlingMoves(
            bool hasKingSideCastlingRights,
            bool hasQueenSideCastlingRights,
            ulong occupied,
            ulong opponentAttacked,
            PlayerEnum player)
        {
            ulong moves = 0;
            if (player == PlayerEnum.White)
            {
                if (CanWhiteKingCastleKingSide(occupied, opponentAttacked, hasKingSideCastlingRights))
                {
                    moves |= CommonBitBoards.WhiteKingCastleKingSide;
                }

                if (CanWhiteKingCastleQueenSide(occupied, opponentAttacked, hasQueenSideCastlingRights))
                {
                    moves |= CommonBitBoards.WhiteKingCastleQueenSide;
                }
            } else
            {
                if (CanBlackKingCastleKingSide(occupied, opponentAttacked, hasKingSideCastlingRights))
                {
                    moves |= CommonBitBoards.BlackKingCastleKingSide;
                }

                if (CanBlackKingCastleQueenSide(occupied, opponentAttacked, hasQueenSideCastlingRights))
                {
                    moves |= CommonBitBoards.BlackKingCastleQueenSide;
                }
            }

            return moves;
        }

        private static bool CanWhiteKingCastleKingSide(ulong occupied, ulong opponentAttacked, bool hasKingSideCastlingRights)
        {   
            // hasKingSideCastlingRights - True if king/kingside rook has never moved
            if (!hasKingSideCastlingRights)
                return false;

            return (CommonBitBoards.WhiteCastleKingSideEmpty & occupied) == 0
                && (CommonBitBoards.WhiteCastleKingSideSafe & opponentAttacked) == 0;
        }

        private static bool CanWhiteKingCastleQueenSide(ulong occupied, ulong opponentAttacked, bool hasQueenSideCastlingRights)
        {   
            // hasKingSideCastlingRights - True if king/kingside rook has never moved
            if (!hasQueenSideCastlingRights)
                return false;

            return (CommonBitBoards.WhiteCastleQueenSideEmpty & occupied) == 0
                && (CommonBitBoards.WhiteCastleQueenSideSafe & opponentAttacked) == 0;
        }

        private static bool CanBlackKingCastleKingSide(ulong occupied, ulong opponentAttacked, bool hasKingSideCastlingRights)
        {   
            // hasKingSideCastlingRights - True if king/kingside rook has never moved
            if (!hasKingSideCastlingRights)
                return false;

            return (CommonBitBoards.BlackCastleKingSideEmpty & occupied) == 0
                && (CommonBitBoards.BlackCastleKingSideSafe & opponentAttacked) == 0;
        }

        private static bool CanBlackKingCastleQueenSide(ulong occupied, ulong opponentAttacked, bool hasQueenSideCastlingRights)
        {   
            // hasKingSideCastlingRights - True if king/kingside rook has never moved
            if (!hasQueenSideCastlingRights)
                return false;

            return (CommonBitBoards.BlackCastleQueenSideEmpty & occupied) == 0
                && (CommonBitBoards.BlackCastleQueenSideSafe & opponentAttacked) == 0;
        }

        private static ulong GenAllBasicKingMoves(ulong bb)
        {
            ulong moves = 0;

            moves |= Shift(bb, N);
            moves |= Shift(bb, S);
            moves |= Shift(bb & CommonBitBoards.NotHFile, E);
            moves |= Shift(bb & CommonBitBoards.NotAFile, W);
            moves |= Shift(bb & CommonBitBoards.NotHFile, NE);
            moves |= Shift(bb & CommonBitBoards.NotAFile, NW);
            moves |= Shift(bb & CommonBitBoards.NotHFile, SE);
            moves |= Shift(bb & CommonBitBoards.NotAFile, SW);

            return moves;
        }

        // Returns all the positions that the player is attacking
        public static ulong GetAttackedBitBoard(BitBoards bbs, PlayerEnum player)
        {
            ulong attacks = 0;
            if (player == PlayerEnum.White)
            {
                attacks |= GenWhitePawnAttacks(bbs.WhitePawns);

                // Should be faster than using shifts provided the number of knights not weirdly high
                while (bbs.WhiteKnights != 0)
                {
                    var tileIndex = bbs.WhiteKnights.GetTrailingZeroCount();
                    attacks |= knightMoves[tileIndex];
                    bbs.WhiteKnights = bbs.WhiteKnights.PopLsb();
                }

                while (bbs.WhiteBishops != 0)
                {
                    var tileIndex = (int)bbs.WhiteBishops.GetTrailingZeroCount();
                    attacks |= MagicBishop.GetMoves(tileIndex, bbs.Occupied);
                    bbs.WhiteBishops = bbs.WhiteBishops.PopLsb();
                }

                while (bbs.WhiteRooks != 0)
                {
                    var tileIndex = (int)bbs.WhiteRooks.GetTrailingZeroCount();
                    attacks |= MagicRook.GetMoves(tileIndex, bbs.Occupied);
                    bbs.WhiteRooks = bbs.WhiteRooks.PopLsb();
                }

                while (bbs.WhiteQueens != 0)
                {
                    var tileIndex = (int)bbs.WhiteQueens.GetTrailingZeroCount();
                    attacks |= MagicBishop.GetMoves(tileIndex, bbs.Occupied);
                    attacks |= MagicRook.GetMoves(tileIndex, bbs.Occupied);
                    bbs.WhiteQueens = bbs.WhiteQueens.PopLsb();
                }

                attacks |= GenAllBasicKingMoves(bbs.WhiteKing);
            } 
            else
            {
                attacks |= GenBlackPawnAttacks(bbs.BlackPawns);
                
                // Should be faster than using shifts provided the number of knights not weirdly high
                while (bbs.BlackKnights != 0)
                {
                    var tileIndex = bbs.BlackKnights.GetTrailingZeroCount();
                    attacks |= knightMoves[tileIndex];
                    bbs.BlackKnights = bbs.BlackKnights.PopLsb();
                }

                while (bbs.BlackBishops != 0)
                {
                    var tileIndex = (int)bbs.BlackBishops.GetTrailingZeroCount();
                    attacks |= MagicBishop.GetMoves(tileIndex, bbs.Occupied);
                    bbs.BlackBishops = bbs.BlackBishops.PopLsb();
                }
                
                while (bbs.BlackRooks != 0)
                {
                    var tileIndex = (int)bbs.BlackRooks.GetTrailingZeroCount();
                    attacks |= MagicRook.GetMoves(tileIndex, bbs.Occupied);
                    bbs.BlackRooks = bbs.BlackRooks.PopLsb();
                }

                while (bbs.BlackQueens != 0)
                {
                    var tileIndex = (int)bbs.BlackQueens.GetTrailingZeroCount();
                    attacks |= MagicBishop.GetMoves(tileIndex, bbs.Occupied);
                    attacks |= MagicRook.GetMoves(tileIndex, bbs.Occupied);
                    bbs.BlackQueens = bbs.BlackQueens.PopLsb();
                }

                attacks |= GenAllBasicKingMoves(bbs.BlackKing);
            }

            return attacks;
        }

        public static bool IsPlayerInCheck(BitBoards bbs, PlayerEnum player)
        {
            var opponent = player == PlayerEnum.White ? PlayerEnum.Black : PlayerEnum.White;

            ulong kingPosition = player == PlayerEnum.White
                ? bbs.WhiteKing
                : bbs.BlackKing;

            ulong attacked = GetAttackedBitBoard(bbs, opponent);

            return (kingPosition & attacked) != 0;
        }

        public static ulong GetMovesInDirection(ulong bb, ulong emptyOrEnemy, ulong notBlocker, int direction)
        {
            // Empty - Can carry on
            // Occupied by player - Not a valid move
            // Occupied by enemy - Valid move but cannot continue any further in direction

            var shift1 = Shift(bb & notBlocker, direction) & emptyOrEnemy;
            var shift2 = Shift(shift1 & notBlocker, direction) & emptyOrEnemy;
            var shift3 = Shift(shift2 & notBlocker, direction) & emptyOrEnemy;
            var shift4 = Shift(shift3 & notBlocker, direction) & emptyOrEnemy;
            var shift5 = Shift(shift4 & notBlocker, direction) & emptyOrEnemy;
            var shift6 = Shift(shift5 & notBlocker, direction) & emptyOrEnemy;
            var shift7 = Shift(shift6 & notBlocker, direction) & emptyOrEnemy;

            return shift1 | shift2 | shift3 | shift4 | shift5 | shift6 | shift7;
        }

        private static ulong Shift(ulong b, int direction)
        {
            return direction > 0 ? b << direction : b >> Math.Abs(direction);
        }
    }
}