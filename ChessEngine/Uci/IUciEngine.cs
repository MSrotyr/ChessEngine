namespace ChessEngine.Uci
{
    public interface IUciEngine
    {
        public string Name {get;}
        public string Author {get;}
        public void UpdateBoard(string[] coordinateMoves);
        public string GetBestMoveCoordinates();
    }
}