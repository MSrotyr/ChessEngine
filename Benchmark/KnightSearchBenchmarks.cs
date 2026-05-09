using BenchmarkDotNet.Attributes;
using ChessEngine;
using ChessEngine.SearchUtils;

namespace Benchmark
{
    public class KnightSearchBenchmarks
    {
        private static Move[] moves = new Move[256];
        private static Board board = new();
        private static ulong emptyOrEnemy = ~board.CurrentBoard.WhiteOccupied;

        public KnightSearchBenchmarks()
        {
        }

        [Benchmark]
        public void GetKnightMovesBenchmark()
        {

            Search.GetKnightMoves(board.CurrentBoard.WhiteKnights, emptyOrEnemy, moves, 0);
        }
    }
}