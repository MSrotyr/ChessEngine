using ChessEngine.Uci;
using ChessEngine.Utils;

namespace ChessEngine
{
    public class RandomEngine : IUciEngine
    {
        private Board board;
        private Random random;
        private bool debugMode;

        public RandomEngine(bool debugMode = false)
        {
            this.board = new Board();
            this.random = new Random();
            this.debugMode = debugMode;
        }

        public string Name => "RandomEngine";
        public string Author => "Matt";

        public string GetBestMoveCoordinates()
        {
            Move[] moves = new Move[256];
            int movesCnt = Search.GetPossibleMoves(board, moves);

            var moveIndex = random.Next(movesCnt);
            var move = moves[0];
            
            string from = PositionParsing.ConvertBitBoardToCoordinatePosition(move.From);
            string to = PositionParsing.ConvertBitBoardToCoordinatePosition(move.To);
            string pp = move.PromotionPeice == PromotionPeice.None
                ? ""
                : Enum.GetName(move.PromotionPeice)!.ToLower();


            return from + to + pp;
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
                new ConsoleChessPrinter().Print(board);
            }
        }
    }
}