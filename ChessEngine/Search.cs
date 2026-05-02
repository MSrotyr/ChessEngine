using ChessEngine.Utils;
using Player;

namespace ChessEngine
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
        static ulong[] knightAttacks = new ulong[64];
        static ulong[] basicKingAttacks = new ulong[64]; // Doesn't include castling

        // Initiate
        static Search()
        {
            for (var i = 0; i < 64; i++)
            {
                knightAttacks[i] = GenAllKnightMoves(1ul << i);
                basicKingAttacks[i] = GenAllBasicKingMoves(1ul << i);
            }
        }

        public static int GetPossibleMoves(Board board, Move[] moves)
        {
            Move[] movesInternal = new Move[256];

            var player = board.GetCurrentPlayer();
            var opponent = player == PlayerEnum.White ? PlayerEnum.Black : PlayerEnum.White;
            ulong empty = ~board.CurrentBoard.Occupied;
            ulong attacked = GetAttackedBitBoard(board.CurrentBoard, player); // TODO only need this if can castle

            int intMoveIndex = 0;
            if (player == PlayerEnum.White)
            {
                ulong emptyOrEnemy = ~board.CurrentBoard.WhiteOccupied;
                ulong enemyOrEnPassant = board.CurrentBoard.BlackOccupied | board.CurrentBoard.EnPassant;
                intMoveIndex = GetWhitePawnMoves(board.CurrentBoard.WhitePawns, empty, enemyOrEnPassant, movesInternal, intMoveIndex);
                intMoveIndex = GetKnightMoves(board.CurrentBoard.WhiteKnights, emptyOrEnemy, movesInternal, intMoveIndex);
                intMoveIndex = GetBishopMoves(board.CurrentBoard.WhiteBishops, emptyOrEnemy, board.CurrentBoard.BlackOccupied, movesInternal, intMoveIndex);
                intMoveIndex = GetRookMoves(board.CurrentBoard.WhiteRooks, emptyOrEnemy, board.CurrentBoard.BlackOccupied, movesInternal, intMoveIndex);
                intMoveIndex = GetQueenMoves(board.CurrentBoard.WhiteQueens, emptyOrEnemy, board.CurrentBoard.BlackOccupied, movesInternal, intMoveIndex);
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
                intMoveIndex = GetBishopMoves(board.CurrentBoard.BlackBishops, emptyOrEnemy, board.CurrentBoard.WhiteOccupied, movesInternal, intMoveIndex);
                intMoveIndex = GetRookMoves(board.CurrentBoard.BlackRooks, emptyOrEnemy, board.CurrentBoard.WhiteOccupied, movesInternal, intMoveIndex);
                intMoveIndex = GetQueenMoves(board.CurrentBoard.BlackQueens, emptyOrEnemy, board.CurrentBoard.WhiteOccupied, movesInternal, intMoveIndex);
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
            foreach(var move in movesInternal)
            {
                board.MakeMove(move);

                ulong kingPosition = player == PlayerEnum.White
                    ? board.CurrentBoard.WhiteKing
                    : board.CurrentBoard.BlackKing;
                    
                ulong atk = GetAttackedBitBoard(board.CurrentBoard, opponent);
                board.UndoLastMove();
                if ((kingPosition & atk) == 0)
                {
                    moves[moveIndex++] = move;
                }
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

        public static ulong GenWhitePawnMoves(ulong bb, ulong empty, ulong enemyOrEnPassant)
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

        public static ulong GenWhitePawnAttacks(ulong bb)
        {
            ulong moves = 0;

            // Diagonal if enemy / En Passant
            moves |= Shift(bb & CommonBitBoards.NotHFile, NE);
            moves |= Shift(bb & CommonBitBoards.NotAFile, NW);
            return moves;
        }

        public static int GetBlackPawnMoves(ulong bb, ulong empty, ulong enemyOrEnPassant, Move[] moves, int moveIndex)
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

        public static ulong GenBlackPawnMoves(ulong bb, ulong empty, ulong enemyOrEnPassant)
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

        public static ulong GenBlackPawnAttacks(ulong bb)
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
                var tileIndex = from.GetTrailingZeroCount();
                ulong attacks = knightAttacks[tileIndex] & emptyOrEnemy;

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

        public static ulong GenAllKnightMoves(ulong bb)
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

        public static int GetBishopMoves(ulong bb, ulong emptyOrEnemy, ulong enemy, Move[] moves, int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                ulong attacks = GenBishopMoves(from, emptyOrEnemy, enemy);

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

        public static ulong GenBishopMoves(ulong bb, ulong emptyOrEnemy, ulong enemy)
        {
            return
                GetMovesInDirection(bb, emptyOrEnemy, enemy, CommonBitBoards.NotHFile, NE) |
                GetMovesInDirection(bb, emptyOrEnemy, enemy, CommonBitBoards.NotHFile, SE) |
                GetMovesInDirection(bb, emptyOrEnemy, enemy, CommonBitBoards.NotAFile, SW) |
                GetMovesInDirection(bb, emptyOrEnemy, enemy, CommonBitBoards.NotAFile, NW);
        }

        public static int GetRookMoves(ulong bb, ulong emptyOrEnemy, ulong enemy, Move[] moves, int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                ulong attacks = GenRookMoves(from, emptyOrEnemy, enemy);

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

        public static ulong GenRookMoves(ulong bb, ulong emptyOrEnemy, ulong enemy)
        {
            return
                GetMovesInDirection(bb, emptyOrEnemy, enemy, CommonBitBoards.AllTiles, N) |
                GetMovesInDirection(bb, emptyOrEnemy, enemy, CommonBitBoards.NotHFile, E) |
                GetMovesInDirection(bb, emptyOrEnemy, enemy, CommonBitBoards.AllTiles, S) |
                GetMovesInDirection(bb, emptyOrEnemy, enemy, CommonBitBoards.NotAFile, W);
        }

        public static int GetQueenMoves(ulong bb, ulong emptyOrEnemy, ulong enemy, Move[] moves, int moveIndex)
        {
            while (bb != 0)
            {
                ulong from = bb.GetLsb();
                ulong attacks = GenQueenMoves(from, emptyOrEnemy, enemy);

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

        public static ulong GenQueenMoves(ulong bb, ulong emptyOrEnemy, ulong enemy)
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
                ulong kingMoves = (basicKingAttacks[tileIndex] & emptyOrEnemy) 
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

        public static ulong GenCastlingMoves(
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

        public static bool CanWhiteKingCastleKingSide(ulong occupied, ulong opponentAttacked, bool hasKingSideCastlingRights)
        {   
            // hasKingSideCastlingRights - True if king/kingside rook has never moved
            if (!hasKingSideCastlingRights)
                return false;

            return (CommonBitBoards.WhiteCastleKingSideEmpty & occupied) == 0
                && (CommonBitBoards.WhiteCastleKingSideSafe & opponentAttacked) == 0;
        }

        public static bool CanWhiteKingCastleQueenSide(ulong occupied, ulong opponentAttacked, bool hasQueenSideCastlingRights)
        {   
            // hasKingSideCastlingRights - True if king/kingside rook has never moved
            if (!hasQueenSideCastlingRights)
                return false;

            return (CommonBitBoards.WhiteCastleQueenSideEmpty & occupied) == 0
                && (CommonBitBoards.WhiteCastleQueenSideSafe & opponentAttacked) == 0;
        }

        public static bool CanBlackKingCastleKingSide(ulong occupied, ulong opponentAttacked, bool hasKingSideCastlingRights)
        {   
            // hasKingSideCastlingRights - True if king/kingside rook has never moved
            if (!hasKingSideCastlingRights)
                return false;

            return (CommonBitBoards.BlackCastleKingSideEmpty & occupied) == 0
                && (CommonBitBoards.BlackCastleKingSideSafe & opponentAttacked) == 0;
        }

        public static bool CanBlackKingCastleQueenSide(ulong occupied, ulong opponentAttacked, bool hasQueenSideCastlingRights)
        {   
            // hasKingSideCastlingRights - True if king/kingside rook has never moved
            if (!hasQueenSideCastlingRights)
                return false;

            return (CommonBitBoards.BlackCastleQueenSideEmpty & occupied) == 0
                && (CommonBitBoards.BlackCastleQueenSideSafe & opponentAttacked) == 0;
        }

        public static ulong GenAllBasicKingMoves(ulong bb)
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
                ulong emptyOrEnemy = ~bbs.WhiteOccupied;
                attacks |= GenWhitePawnAttacks(bbs.WhitePawns);
                attacks |= GenAllKnightMoves(bbs.WhiteKnights);
                attacks |= GenBishopMoves(bbs.WhiteBishops, emptyOrEnemy, bbs.BlackOccupied);
                attacks |= GenRookMoves(bbs.WhiteRooks, emptyOrEnemy, bbs.BlackOccupied);
                attacks |= GenQueenMoves(bbs.WhiteQueens, emptyOrEnemy, bbs.BlackOccupied);
                attacks |= GenAllBasicKingMoves(bbs.WhiteKing);
            } 
            else
            {
                ulong emptyOrEnemy = ~bbs.BlackOccupied;
                attacks |= GenBlackPawnAttacks(bbs.BlackPawns);
                attacks |= GenAllKnightMoves(bbs.BlackKnights);
                attacks |= GenBishopMoves(bbs.BlackBishops, emptyOrEnemy, bbs.WhiteOccupied);
                attacks |= GenRookMoves(bbs.BlackRooks, emptyOrEnemy, bbs.WhiteOccupied);
                attacks |= GenQueenMoves(bbs.BlackQueens, emptyOrEnemy, bbs.WhiteOccupied);
                attacks |= GenAllBasicKingMoves(bbs.BlackKing);
            }

            return attacks;
        }

        // TODO Return Early possibly quick performance boost
        public static ulong GetMovesInDirection(ulong bb, ulong emptyOrEnemy, ulong enemy, ulong notBarrierFile, int direction)
        {
            // Empty - Can carry on
            // Occupied by player - Not a valid move
            // Occupied by enemy - Valid move but cannot continue any further in direction

            var notEnemy = ~enemy;

            var shift1 = Shift(bb & notBarrierFile, direction) & emptyOrEnemy;
            var shift2 = Shift(shift1 & notBarrierFile & notEnemy, direction) & emptyOrEnemy;
            var shift3 = Shift(shift2 & notBarrierFile & notEnemy, direction) & emptyOrEnemy;
            var shift4 = Shift(shift3 & notBarrierFile & notEnemy, direction) & emptyOrEnemy;
            var shift5 = Shift(shift4 & notBarrierFile & notEnemy, direction) & emptyOrEnemy;
            var shift6 = Shift(shift5 & notBarrierFile & notEnemy, direction) & emptyOrEnemy;
            var shift7 = Shift(shift6 & notBarrierFile & notEnemy, direction) & emptyOrEnemy;

            return shift1 | shift2 | shift3 | shift4 | shift5 | shift6 | shift7;
        }

        public static ulong Shift(ulong b, int direction)
        {
            return direction > 0 ? b << direction : b >> Math.Abs(direction);
        }
    }
}