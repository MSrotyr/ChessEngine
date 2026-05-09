using BenchmarkDotNet.Attributes;
using ChessEngine;
using ChessEngine.SearchUtils;

namespace Benchmark
{
    public class SearchBenchmarks
    {
        private static Move[] moves = new Move[256];
        private static Board board = new();
        private static ulong empty = ~board.CurrentBoard.Occupied;
        private static ulong enemyOrEnPassant = board.CurrentBoard.BlackOccupied | board.CurrentBoard.EnPassant;
        private static ulong emptyOrEnemy = ~board.CurrentBoard.WhiteOccupied;
        private static ulong attacked = Search.GetAttackedBitBoard(board.CurrentBoard, Player.PlayerEnum.White);
        private static Move[] possibleMoves = new Move[256];
        private static int possibleMovesCnt = Search.GetPossibleMoves(board, possibleMoves);

        public SearchBenchmarks()
        {
            MagicRook.Initialize(MagicNumbers.RookMagicNumbers);
        }

        [Benchmark]
        public void GetPossibleMovesBenchmark()
        {
            Search.GetPossibleMoves(new Board(), moves);
        }

        // [Benchmark]
        // public void GetWhitePawnMovesBenchmark()
        // {

        //     Search.GetWhitePawnMoves(board.CurrentBoard.WhitePawns, empty, enemyOrEnPassant, moves, 0);
        // }

        // [Benchmark]
        // public void GetBishopMovesBenchmark()
        // {

        //     Search.GetBishopMoves(board.CurrentBoard.WhiteBishops, emptyOrEnemy, board.CurrentBoard.BlackOccupied, moves, 0);
        // }

        // [Benchmark]
        // public void GetQueenMovesBenchmark()
        // {

        //     Search.GetQueenMoves(board.CurrentBoard.WhiteQueens, emptyOrEnemy, board.CurrentBoard.BlackOccupied, moves, 0);
        // }

        // [Benchmark]
        // public void GetKingMovesBenchmark()
        // {

        //     Search.GetKingMoves(board.CurrentBoard.WhiteKing, emptyOrEnemy, attacked, board.CurrentBoard.Occupied, board.CurrentBoard.HasWhiteKingSideCastlingRights, board.CurrentBoard.HasWhiteQueenSideCastlingRights, Player.PlayerEnum.White, moves, 0);
        // }

        // All moves will pass in starting position
        [Benchmark]
        public void CheckFiltrationBenchmark()
        {
            int moveIndex = 0;
            for (var i = 0; i < possibleMovesCnt; i++)
            {
                board.MakeMove(possibleMoves[i]);
                if (!Search.IsPlayerInCheck(board.CurrentBoard, Player.PlayerEnum.White))
                {
                    moves[moveIndex++] = possibleMoves[i];
                }
                board.UndoLastMove();
            }
        }
    }
}