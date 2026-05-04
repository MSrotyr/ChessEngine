using System.Reflection.Metadata;
using ChessEngine.SearchUtils;
using Player;

namespace ChessEngine
{
    public class Board
    {
        public BitBoards CurrentBoard;
        public Stack<BitBoards> MoveHistory = new();

        public Board()
        {
            CurrentBoard = BitBoards.StartPosition();
        }

        public PlayerEnum GetCurrentPlayer()
        {
            return MoveHistory.Count % 2 == 0 ? PlayerEnum.White : PlayerEnum.Black;
        }

        public void UndoLastMove()
        {
            CurrentBoard = MoveHistory.Pop();
        }

        public void MakeMove(Move move)
        {
            var oldBoard = CurrentBoard;

            if (GetCurrentPlayer() == PlayerEnum.White)
            {
                if ((CurrentBoard.WhitePawns & move.From) != 0)
                {
                    if (move.PromotionPeice != PromotionPeice.None)
                    {
                        // Pawn promotion - Remove pawn & add new peice
                        CurrentBoard.WhitePawns &= ~move.From;

                        switch (move.PromotionPeice)
                        {
                            case PromotionPeice.N:
                                CurrentBoard.WhiteKnights |= move.To;
                                break;
                            case PromotionPeice.B:
                                CurrentBoard.WhiteBishops |= move.To;
                                break;
                            case PromotionPeice.R:
                                CurrentBoard.WhiteRooks |= move.To;
                                break;
                            case PromotionPeice.Q:
                                CurrentBoard.WhiteQueens |= move.To;
                                break;
                        }

                    } else
                    {
                        CurrentBoard.WhitePawns = (CurrentBoard.WhitePawns & ~move.From) | move.To;
                    }

                    // handle en passant
                    if ((move.To & CurrentBoard.EnPassant) != 0)
                    {
                        CurrentBoard.BlackPawns &= ~(move.To >> 8);
                    }

                    // Check for possible EnPassant Moves for the next turn
                    else if ((move.From & CommonBitBoards.Rank2) != 0 && (move.To & CommonBitBoards.Rank4) != 0)
                    {
                        CurrentBoard.EnPassant = move.From << 8;
                    }
                }

                else if ((CurrentBoard.WhiteKnights & move.From) != 0)
                {
                    CurrentBoard.WhiteKnights = (CurrentBoard.WhiteKnights & ~move.From) | move.To;
                }

                else if ((CurrentBoard.WhiteBishops & move.From) != 0)
                {
                    CurrentBoard.WhiteBishops = (CurrentBoard.WhiteBishops & ~move.From) | move.To;
                }

                else if ((CurrentBoard.WhiteRooks & move.From) != 0)
                {
                    CurrentBoard.WhiteRooks = (CurrentBoard.WhiteRooks & ~move.From) | move.To;

                    if (move.From == CommonBitBoards.WhiteRookKingSideHome) {
                        CurrentBoard.HasWhiteKingSideCastlingRights = false;
                    }

                    else if (move.From == CommonBitBoards.WhiteRookQueenSideHome) {
                        CurrentBoard.HasWhiteKingSideCastlingRights = false;
                    }
                }

                else if ((CurrentBoard.WhiteQueens & move.From) != 0)
                {
                    CurrentBoard.WhiteQueens = (CurrentBoard.WhiteQueens & ~move.From) | move.To;
                }

                else if ((CurrentBoard.WhiteKing & move.From) != 0)
                {
                    CurrentBoard.WhiteKing = (CurrentBoard.WhiteKing & ~move.From) | move.To;
                    CurrentBoard.HasWhiteKingSideCastlingRights = false;
                    CurrentBoard.HasWhiteQueenSideCastlingRights = false;

                    // Move rook if castled
                    if (move.From == CommonBitBoards.WhiteKingHome)
                    {
                        if (move.To == CommonBitBoards.WhiteKingCastleKingSide)
                        {
                            CurrentBoard.WhiteRooks = (CurrentBoard.WhiteRooks & ~CommonBitBoards.WhiteRookKingSideHome) | CommonBitBoards.WhiteRookCastleKingSide;
                        }

                        else if (move.To == CommonBitBoards.WhiteKingCastleQueenSide)
                        {
                            CurrentBoard.WhiteRooks = (CurrentBoard.WhiteRooks & ~CommonBitBoards.WhiteRookQueenSideHome) | CommonBitBoards.WhiteRookCastleQueenSide;
                        }
                    }
                }

                CurrentBoard.BlackPawns &= ~move.To;
                CurrentBoard.BlackKnights &= ~move.To;
                CurrentBoard.BlackBishops &= ~move.To;
                CurrentBoard.BlackRooks &= ~move.To;
                CurrentBoard.BlackQueens &= ~move.To;
                CurrentBoard.BlackKing &= ~move.To;
            } else 
            {
                if ((CurrentBoard.BlackPawns & move.From) != 0)
                {
                    if (move.PromotionPeice != PromotionPeice.None)
                    {
                        // Pawn promotion - Remove pawn & add new peice
                        CurrentBoard.BlackPawns &= ~move.From;

                        switch (move.PromotionPeice)
                        {
                            case PromotionPeice.N:
                                CurrentBoard.BlackKnights |= move.To;
                                break;
                            case PromotionPeice.B:
                                CurrentBoard.BlackBishops |= move.To;
                                break;
                            case PromotionPeice.R:
                                CurrentBoard.BlackRooks |= move.To;
                                break;
                            case PromotionPeice.Q:
                                CurrentBoard.BlackQueens |= move.To;
                                break;
                        }

                    } else
                    {
                        CurrentBoard.BlackPawns = (CurrentBoard.BlackPawns & ~move.From) | move.To;
                    }

                    // handle en passant
                    if ((move.To & CurrentBoard.EnPassant) != 0)
                    {
                        CurrentBoard.WhitePawns &= ~(move.To << 8);
                    }

                    // Check for possible EnPassant Moves for the next turn
                    else if ((move.From & CommonBitBoards.Rank7) != 0 && (move.To & CommonBitBoards.Rank5) != 0)
                    {
                        CurrentBoard.EnPassant = move.From >> 8;
                    }
                }

                else if ((CurrentBoard.BlackKnights & move.From) != 0)
                {
                    CurrentBoard.BlackKnights = (CurrentBoard.BlackKnights & ~move.From) | move.To;
                }

                else if ((CurrentBoard.BlackBishops & move.From) != 0)
                {
                    CurrentBoard.BlackBishops = (CurrentBoard.BlackBishops & ~move.From) | move.To;
                }

                else if ((CurrentBoard.BlackRooks & move.From) != 0)
                {
                    CurrentBoard.BlackRooks = (CurrentBoard.BlackRooks & ~move.From) | move.To;

                    if (move.From == CommonBitBoards.BlackRookKingSideHome) {
                        CurrentBoard.HasBlackKingSideCastlingRights = false;
                    }

                    else if (move.From == CommonBitBoards.BlackRookQueenSideHome) {
                        CurrentBoard.HasBlackKingSideCastlingRights = false;
                    }
                }

                else if ((CurrentBoard.BlackQueens & move.From) != 0)
                {
                    CurrentBoard.BlackQueens = (CurrentBoard.BlackQueens & ~move.From) | move.To;
                }

                else if ((CurrentBoard.BlackKing & move.From) != 0)
                {
                    CurrentBoard.BlackKing = (CurrentBoard.BlackKing & ~move.From) | move.To;
                    CurrentBoard.HasBlackKingSideCastlingRights = false;
                    CurrentBoard.HasBlackQueenSideCastlingRights = false;

                    // Move rook if castled
                    if (move.From == CommonBitBoards.BlackKingHome)
                    {
                        if (move.To == CommonBitBoards.BlackKingCastleKingSide)
                        {
                            CurrentBoard.BlackRooks = (CurrentBoard.BlackRooks & ~CommonBitBoards.BlackRookKingSideHome) | CommonBitBoards.BlackRookCastleKingSide;
                        }

                        else if (move.To == CommonBitBoards.BlackKingCastleQueenSide)
                        {
                            CurrentBoard.BlackRooks = (CurrentBoard.BlackRooks & ~CommonBitBoards.BlackRookQueenSideHome) | CommonBitBoards.BlackRookCastleQueenSide;
                        }
                    }
                }

                CurrentBoard.WhitePawns &= ~move.To;
                CurrentBoard.WhiteKnights &= ~move.To;
                CurrentBoard.WhiteBishops &= ~move.To;
                CurrentBoard.WhiteRooks &= ~move.To;
                CurrentBoard.WhiteQueens &= ~move.To;
                CurrentBoard.WhiteKing &= ~move.To;
            }

            // Derived BitBoards
            CurrentBoard.WhiteOccupied =
                CurrentBoard.WhitePawns | CurrentBoard.WhiteKnights | CurrentBoard.WhiteBishops |
                CurrentBoard.WhiteRooks | CurrentBoard.WhiteQueens | CurrentBoard.WhiteKing;

            CurrentBoard.BlackOccupied =
                CurrentBoard.BlackPawns | CurrentBoard.BlackKnights | CurrentBoard.BlackBishops |
                CurrentBoard.BlackRooks | CurrentBoard.BlackQueens | CurrentBoard.BlackKing;

            CurrentBoard.Occupied = CurrentBoard.WhiteOccupied | CurrentBoard.BlackOccupied;

            // Reset en passant if not set by the current move
            if (CurrentBoard.EnPassant == oldBoard.EnPassant)
            {
                CurrentBoard.EnPassant = 0UL;
            }

            MoveHistory.Push(oldBoard);
        }
    }
}