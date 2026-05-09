using ChessEngine;
using ChessEngine.SearchUtils;

namespace ChessEngineTests;

public class SearchTests
{
    public SearchTests()
    {
        MagicRook.Initialize(MagicNumbers.RookMagicNumbers);
    }


    [Fact]
    public void GetPossibleMovesTest()
    {
        var board = new Board();
        var moves = new Move[256];
        int movesCnt = Search.GetPossibleMoves(board, moves);

        Assert.Equal(20, movesCnt);
    }
}
