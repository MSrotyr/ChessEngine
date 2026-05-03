using ChessEngine.Utils;

namespace ChessEngine.Evaluators
{
    public class PeiceCounterEvaluator : IZeroDepthEvaluator
    {
        private const double knightValue = 3;
        private const double bishopValue = 3.1;
        private const double rookValue = 5;
        private const double queenValue = 9;
        private const double kingValue = 1000;
        public double Evaluate(BitBoards bbs)
        {
            double whiteValue =
                bbs.WhitePawns.GetNumSetBits() +
                (bbs.WhiteKnights.GetNumSetBits() * knightValue) +
                (bbs.WhiteBishops.GetNumSetBits() * bishopValue) +
                (bbs.WhiteRooks.GetNumSetBits() * rookValue) +
                (bbs.WhiteQueens.GetNumSetBits() * queenValue) +
                (bbs.WhiteKing.GetNumSetBits() * kingValue);

            double blackValue =
                bbs.BlackPawns.GetNumSetBits() +
                (bbs.BlackKnights.GetNumSetBits() * knightValue) +
                (bbs.BlackBishops.GetNumSetBits() * bishopValue) +
                (bbs.BlackRooks.GetNumSetBits() * rookValue) +
                (bbs.BlackQueens.GetNumSetBits() * queenValue) +
                (bbs.BlackKing.GetNumSetBits() * kingValue);

            return whiteValue - blackValue;
        }
    }
}