using BenchmarkDotNet.Attributes;
using ChessEngine;
using ChessEngine.SearchUtils;

namespace Benchmark
{
    [MarkdownExporterAttribute.GitHub]
    public class BishopSearchBenchmarks
    {
        private static Move[] moves = new Move[256];
        private static Board board = new();
        private static ulong emptyOrEnemy = ~board.CurrentBoard.WhiteOccupied;

        public BishopSearchBenchmarks()
        {
            MagicBishop.Initialize(MagicNumbers.BishopMagicNumbers);
        }

        [Benchmark(Baseline = true)]
        public void GetBishopMovesBenchmark()
        {
            Search.GetBishopMoves(board.CurrentBoard.WhiteBishops, emptyOrEnemy, board.CurrentBoard.Occupied, moves, 0);
        }
    }
}