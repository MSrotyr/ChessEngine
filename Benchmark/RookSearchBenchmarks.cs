using BenchmarkDotNet.Attributes;
using ChessEngine;
using ChessEngine.SearchUtils;

namespace Benchmark
{
    [MarkdownExporterAttribute.GitHub]
    public class RookSearchBenchmarks
    {
        private static Move[] moves = new Move[256];
        private static Board board = new();
        private static ulong emptyOrEnemy = ~board.CurrentBoard.WhiteOccupied;

        public RookSearchBenchmarks()
        {
            MagicRook.Initialize(MagicNumbers.RookMagicNumbers);
        }

        [Benchmark(Baseline = true)]
        public void GetRookMovesBenchmark()
        {
            Search.GetRookMoves(board.CurrentBoard.WhiteRooks, emptyOrEnemy, board.CurrentBoard.Occupied, moves, 0);
        }
    }
}