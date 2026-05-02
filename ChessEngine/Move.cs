namespace ChessEngine
{
    public struct Move
    {
        public Move(ulong from, ulong to, PromotionPeice promotionPeice = PromotionPeice.None) : this()
        {
            From = from;
            To = to;
            PromotionPeice = promotionPeice;
        }
        public ulong From;
        public ulong To;
        public PromotionPeice PromotionPeice;
    }
}