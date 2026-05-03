namespace ChessEngine.Evaluators
{
    /// <summary>
    /// Represents a simple evaluator that evaluates a position solely on current board state and not future moves
    /// </summary>
    public interface IZeroDepthEvaluator
    {
        double Evaluate(BitBoards bbs);
    }
}