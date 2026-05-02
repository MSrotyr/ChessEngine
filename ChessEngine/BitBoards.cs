public struct BitBoards
{
    public ulong WhitePawns;
    public ulong WhiteKnights;
    public ulong WhiteBishops;
    public ulong WhiteRooks;
    public ulong WhiteQueens;
    public ulong WhiteKing;

    public ulong BlackPawns;
    public ulong BlackKnights;
    public ulong BlackBishops;
    public ulong BlackRooks;
    public ulong BlackQueens;
    public ulong BlackKing;

    // Derived
    public ulong WhiteOccupied;
    public ulong BlackOccupied;
    public ulong Occupied;
    /// <summary>
    /// Identifies all the en passant attacks sqaures
    /// </summary>
    public ulong EnPassant;
    public bool HasWhiteKingSideCastlingRights;
    public bool HasWhiteQueenSideCastlingRights;
    public bool HasBlackKingSideCastlingRights;
    public bool HasBlackQueenSideCastlingRights;

    public static BitBoards StartPosition()
    {
        var b = new BitBoards
        {
            WhitePawns   = 0x000000000000FF00UL,
            WhiteKnights = 0x0000000000000042UL,
            WhiteBishops = 0x0000000000000024UL,
            WhiteRooks   = 0x0000000000000081UL,
            WhiteQueens  = 0x0000000000000008UL,
            WhiteKing    = 0x0000000000000010UL,

            BlackPawns   = 0x00FF000000000000UL,
            BlackKnights = 0x4200000000000000UL,
            BlackBishops = 0x2400000000000000UL,
            BlackRooks   = 0x8100000000000000UL,
            BlackQueens  = 0x0800000000000000UL,
            BlackKing    = 0x1000000000000000UL
        };

        b.WhiteOccupied =
        b.WhitePawns | b.WhiteKnights | b.WhiteBishops |
        b.WhiteRooks | b.WhiteQueens | b.WhiteKing;

        b.BlackOccupied =
            b.BlackPawns | b.BlackKnights | b.BlackBishops |
            b.BlackRooks | b.BlackQueens | b.BlackKing;

        b.Occupied = b.WhiteOccupied | b.BlackOccupied;

        b.EnPassant = 0UL;

        b.HasWhiteKingSideCastlingRights = true;
        b.HasWhiteQueenSideCastlingRights = true;
        b.HasBlackKingSideCastlingRights = true;
        b.HasBlackQueenSideCastlingRights = true;


        return b;
    }
}