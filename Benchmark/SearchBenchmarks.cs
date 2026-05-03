using BenchmarkDotNet.Attributes;
using ChessEngine;

namespace Benchmark
{
    public class SearchBenchmarks
    {
        private static Move[] moves = new Move[256];
        private static readonly Board board = new();
        private static readonly ulong empty = ~board.CurrentBoard.Occupied;
        private static readonly ulong enemyOrEnPassant = board.CurrentBoard.BlackOccupied | board.CurrentBoard.EnPassant;
        private static readonly ulong emptyOrEnemy = ~board.CurrentBoard.WhiteOccupied;
        private static readonly ulong attacked = Search.GetAttackedBitBoard(board.CurrentBoard, Player.PlayerEnum.White);

        public SearchBenchmarks()
        {
        }

        [Benchmark]
        public void GetPossibleMovesBenchmark()
        {
            Search.GetPossibleMoves(new Board(), moves);
        }

        [Benchmark]
        public void GetWhitePawnMovesBenchmark()
        {
            
            Search.GetWhitePawnMoves(board.CurrentBoard.WhitePawns, empty, enemyOrEnPassant, moves, 0);
        }

        [Benchmark]
        public void GetKnightMovesBenchmark()
        {
            
            Search.GetKnightMoves(board.CurrentBoard.WhiteKnights, emptyOrEnemy, moves, 0);
        }

        [Benchmark]
        public void GetBishopMovesBenchmark()
        {
            
            Search.GetBishopMoves(board.CurrentBoard.WhiteBishops, emptyOrEnemy, board.CurrentBoard.BlackOccupied, moves, 0);
        }

        [Benchmark]
        public void GetRookMovesBenchmark()
        {
            
            Search.GetRookMoves(board.CurrentBoard.WhiteRooks, emptyOrEnemy, board.CurrentBoard.BlackOccupied, moves, 0);
        }

        [Benchmark]
        public void GetQueenMovesBenchmark()
        {
            
            Search.GetQueenMoves(board.CurrentBoard.WhiteQueens, emptyOrEnemy, board.CurrentBoard.BlackOccupied, moves, 0);
        }

        [Benchmark]
        public void GetKingMovesBenchmark()
        {
            
            Search.GetKingMoves(board.CurrentBoard.WhiteKing, emptyOrEnemy, attacked, board.CurrentBoard.Occupied, board.CurrentBoard.HasWhiteKingSideCastlingRights, board.CurrentBoard.HasWhiteQueenSideCastlingRights, Player.PlayerEnum.White, moves, 0);
        }
    }
}