namespace ChessEngine
{
    public static class ConsoleChessPrinter
    {
        public static void Print(Board b)
        {
            for (int rank = 7; rank >= 0; rank--)
            {
                Console.Write(rank + 1 + "  ");

                for (int file = 0; file < 8; file++)
                {
                    int sq = rank * 8 + file;
                    ulong mask = 1UL << sq;

                    char piece = GetPieceChar(b.CurrentBoard, mask);
                    Console.Write(piece + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\n   a b c d e f g h");
        }

        public static void PrintBitboard(ulong bitboard)
        {
            for (int rank = 7; rank >= 0; rank--)
            {
                Console.Write(rank + 1 + "  ");

                for (int file = 0; file < 8; file++)
                {
                    int square = rank * 8 + file;

                    ulong mask = 1UL << square;

                    Console.Write((bitboard & mask) != 0 ? "1 " : "0 ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static string BitboardToString(ulong bitboard)
        {
            var str = "";
            for (int rank = 7; rank >= 0; rank--)
            {
                str += rank + 1 + "  ";

                for (int file = 0; file < 8; file++)
                {
                    int square = rank * 8 + file;

                    ulong mask = 1UL << square;

                    str += (bitboard & mask) != 0 ? "1 " : "0 ";
                }

                str += Environment.NewLine;
            }
            str += Environment.NewLine;

            return str;
        }

        private static char GetPieceChar(BitBoards b, ulong sq)
        {
            if ((b.WhitePawns & sq) != 0) return 'P';
            if ((b.WhiteKnights & sq) != 0) return 'N';
            if ((b.WhiteBishops & sq) != 0) return 'B';
            if ((b.WhiteRooks & sq) != 0) return 'R';
            if ((b.WhiteQueens & sq) != 0) return 'Q';
            if ((b.WhiteKing & sq) != 0) return 'K';

            if ((b.BlackPawns & sq) != 0) return 'p';
            if ((b.BlackKnights & sq) != 0) return 'n';
            if ((b.BlackBishops & sq) != 0) return 'b';
            if ((b.BlackRooks & sq) != 0) return 'r';
            if ((b.BlackQueens & sq) != 0) return 'q';
            if ((b.BlackKing & sq) != 0) return 'k';

            return '.';
        }
    }
}