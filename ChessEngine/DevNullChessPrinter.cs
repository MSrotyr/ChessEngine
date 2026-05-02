namespace ChessEngine
{
    public class DevNullPrinter : IChessPrinter
    {
        public void Print(Board b)
        {
        }

        public void PrintBitboard(ulong bitboard)
        {
        }
    }
}