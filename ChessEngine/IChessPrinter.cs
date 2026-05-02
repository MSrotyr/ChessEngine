namespace ChessEngine
{
    public interface IChessPrinter
    {
        void Print(Board b);
        void PrintBitboard(ulong bitboard);
    }
}