using ChessEngine.Evaluators;
using ChessEngine.Uci;
using ChessEngine.Utils;

namespace ChessEngine
{
    public class Engine : IUciEngine
    {
        private Board board;
        private readonly DepthSearchEvaluator evaluator;
        private readonly bool debugMode;

        public Engine(DepthSearchEvaluator evaluator, bool debugMode = false)
        {
            board = new Board();
            this.debugMode = debugMode;
            this.evaluator = evaluator;
        }

        public string Name => "RandomEngine";
        public string Author => "Matt";

        public (string Move, double Eval) GetBestMoveCoordinates()
        {
            var result = evaluator.Evaluate(board, 5);
            var move = result.BestMove;
            
            string from = PositionParsing.ConvertBitBoardToCoordinatePosition(move.From);
            string to = PositionParsing.ConvertBitBoardToCoordinatePosition(move.To);
            string pp = move.PromotionPeice == PromotionPeice.None
                ? ""
                : Enum.GetName(move.PromotionPeice)!.ToLower();


            return (from + to + pp, result.Evaluation);
        }

        public void UpdateBoard(string[] coordinateMoves)
        {
            // Reset Board
            board = new Board();

            foreach(string coord in coordinateMoves)
            {
                ulong from = PositionParsing.ConvertCoordinatePositionToBitBoard(coord[0..2]);
                ulong to = PositionParsing.ConvertCoordinatePositionToBitBoard(coord[2..4]);
                PromotionPeice pp = coord.Length == 5 ? Enum.Parse<PromotionPeice>(coord[4].ToString(), true) : PromotionPeice.None;

                var move = new Move(from, to, pp);
                board.MakeMove(move);
            }

            if (debugMode)
            {
                ConsoleChessPrinter.Print(board);
            }
        }
    }
}