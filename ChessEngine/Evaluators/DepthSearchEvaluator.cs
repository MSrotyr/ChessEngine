using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using ChessEngine.Utils;

namespace ChessEngine.Evaluators
{
    public class DepthSearchEvaluator
    {
        private IZeroDepthEvaluator zeroDepthEvaluator;

        public DepthSearchEvaluator(IZeroDepthEvaluator zeroDepthEvaluator)
        {
            this.zeroDepthEvaluator = zeroDepthEvaluator;
        }

        public DepthSearchEvalResult Evaluate(Board board, int depth)
        {
            if (depth < 1)
            {
                throw new Exception("Zero depth evaluation requested");
            }

            Move[] moves = new Move[256];
            int movesCnt = Search.GetPossibleMoves(board, moves);
            var player = board.GetCurrentPlayer();

            double[] res = new double[256];
            Parallel.For(0, movesCnt, i =>
            {
                var boardCopy = new Board
                {
                    CurrentBoard = board.CurrentBoard,
                    MoveHistory = new Stack<BitBoards>(board.MoveHistory)
                };

                // DEBUG
                // var from = PositionParsing.ConvertBitBoardToCoordinatePosition(moves[i].From);
                // var to = PositionParsing.ConvertBitBoardToCoordinatePosition(moves[i].To);

                boardCopy.MakeMove(moves[i]);
                res[i] = EvaluateInternal(boardCopy, depth - 1);
                boardCopy.UndoLastMove();
            });

            int bestMoveIndex = 0;
            double bestMoveEval = player == Player.PlayerEnum.White ? double.MinValue : double.MaxValue;
            for (var i = 0; i < movesCnt; i++)
            {
                // DEBUG
                var from = PositionParsing.ConvertBitBoardToCoordinatePosition(moves[i].From);
                var to = PositionParsing.ConvertBitBoardToCoordinatePosition(moves[i].To);

                var eval = res[i];

                if (player == Player.PlayerEnum.White && eval > bestMoveEval)
                {
                    bestMoveIndex = i;
                    bestMoveEval = eval;
                } 
                
                else if (player == Player.PlayerEnum.Black && eval < bestMoveEval) {
                    bestMoveIndex = i;
                    bestMoveEval = eval;
                }
            }

            return new DepthSearchEvalResult(moves[bestMoveIndex], bestMoveEval);
        }

        private double EvaluateInternal(Board board, int depth)
        {
            if (depth == 0)
            {
                return zeroDepthEvaluator.Evaluate(board.CurrentBoard);
            }

            Move[] moves = new Move[256];
            int movesCnt = Search.GetPossibleMoves(board, moves);
            var player = board.GetCurrentPlayer();
            double bestMoveEval = player == Player.PlayerEnum.White ? double.MinValue : double.MaxValue;

            // Check if game is over
            // if player in check => player has lost
            // else stalemate => draw
            if (movesCnt == 0)
            {
                return Search.IsPlayerInCheck(board.CurrentBoard, player) ? bestMoveEval : 0;
            }

            for (var i = 0; i < movesCnt; i++)
            {
                // DEBUG
                var from = PositionParsing.ConvertBitBoardToCoordinatePosition(moves[i].From);
                var to = PositionParsing.ConvertBitBoardToCoordinatePosition(moves[i].To);

                board.MakeMove(moves[i]);
                var eval = EvaluateInternal(board, depth - 1);

                if (player == Player.PlayerEnum.White && eval > bestMoveEval)
                {
                    bestMoveEval = eval;
                } 
                
                else if (player == Player.PlayerEnum.Black && eval < bestMoveEval) {
                    bestMoveEval = eval;
                }

                board.UndoLastMove();
            }

            return bestMoveEval;
        }
    }
}