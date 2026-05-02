namespace ChessEngine.Utils
{
    public static class CommonBitBoards
    {
        public const ulong NotAFile = 0xFEFEFEFEFEFEFEFEUL;
        public const ulong NotABFile = 0xFCFCFCFCFCFCFCFCUL;
        public const ulong NotHFile = 0x7F7F7F7F7F7F7F7FUL;
        public const ulong NotGHFile = 0x3F3F3F3F3F3F3F3FUL;
        public const ulong AllTiles = 0xFFFFFFFFFFFFFFFF;

        // Ranks
        public const ulong Rank1 = 0x00000000000000FFUL;
        public const ulong Rank2 = 0x000000000000FF00UL;
        public const ulong Rank3 = 0x0000000000FF0000UL;
        public const ulong Rank4 = 0x00000000FF000000UL;
        public const ulong Rank5 = 0x000000FF00000000UL;
        public const ulong Rank6 = 0x0000FF0000000000UL;
        public const ulong Rank7 = 0x00FF000000000000UL;
        public const ulong Rank8 = 0xFF00000000000000UL;

        // King
        public const ulong WhiteKingHome = 0x0000000000000010UL; // E1
        public const ulong BlackKingHome = 0x1000000000000000UL;
        public const ulong WhiteKingCastleKingSide = 0x0000000000000040UL; // G1
        public const ulong WhiteKingCastleQueenSide = 0x0000000000000004UL; // C1
        public const ulong BlackKingCastleKingSide = 0x4000000000000000UL; // G8
        public const ulong BlackKingCastleQueenSide = 0x0400000000000000UL; // C8
        public const ulong WhiteCastleKingSideSafe = 0x0000000000000070UL; // (E1, F1, G1)
        public const ulong WhiteCastleKingSideEmpty = 0x0000000000000060UL; // (F1, G1)
        public const ulong WhiteCastleQueenSideSafe = 0x000000000000001CUL; // (E1, D1, C1)
        public const ulong WhiteCastleQueenSideEmpty = 0x000000000000000EUL; // (D1, C1, B1)
        public const ulong BlackCastleKingSideSafe = 0x7000000000000000UL; // (E8, F8, G8)
        public const ulong BlackCastleKingSideEmpty = 0x6000000000000000UL; // (F8, G8)
        public const ulong BlackCastleQueenSideSafe = 0x1C00000000000000UL; // (E8, D8, C8)
        public const ulong BlackCastleQueenSideEmpty = 0x0E00000000000000UL; // (D8, C8, B8)

        // Rook
        public const ulong WhiteRookQueenSideHome = 0x0000000000000001UL; // A1
        public const ulong WhiteRookKingSideHome  = 0x0000000000000080UL; // H1
        public const ulong BlackRookQueenSideHome = 0x0100000000000000UL; // A8
        public const ulong BlackRookKingSideHome  = 0x8000000000000000UL; // H8
        public const ulong WhiteRookCastleKingSide = 0x0000000000000020UL; // F1
        public const ulong WhiteRookCastleQueenSide = 0x0000000000000008UL; // D1
        public const ulong BlackRookCastleKingSide = 0x2000000000000000UL; // F8
        public const ulong BlackRookCastleQueenSide = 0x0800000000000000UL; // D8
    }
}