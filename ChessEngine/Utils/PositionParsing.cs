namespace ChessEngine.Utils
{
    public static class PositionParsing
    {
        private static Dictionary<string, int> fileLetterToIndexMap = new()
        {
            {"a", 0},
            {"b", 1},
            {"c", 2},
            {"d", 3},
            {"e", 4},
            {"f", 5},
            {"g", 6},
            {"h", 7},
        };

        private static Dictionary<int, string> fileIndexToLetterMap = 
            fileLetterToIndexMap.ToDictionary(x => x.Value, x => x.Key);

        // e.g. e1
        public static ulong ConvertCoordinatePositionToBitBoard(string coord)
        {
            int index = fileLetterToIndexMap[coord[0].ToString()] + 8 * (int.Parse(coord[1].ToString()) - 1);
            return 1ul << index;
        }

        public static string ConvertBitBoardToCoordinatePosition(ulong bb)
        {
            var index = (int)bb.GetTrailingZeroCount();
            var rankIndex = (int)Math.Floor((double)index / 8);
            var fileIndex = index % 8;

            return fileIndexToLetterMap[fileIndex] + (rankIndex + 1);
        }
    }
}