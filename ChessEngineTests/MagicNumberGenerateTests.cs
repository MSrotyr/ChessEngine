using ChessEngine.SearchUtils;

namespace ChessEngineTests
{
    public class MagicNumberGenerateTests
    {
        [Fact(Skip = "Use to generate rook magic numbers")]
        public void GenRookMagicNumFile()
        {
            var magic = MagicRook.Initialize();
            var str = "";

            for(var i = 0; i < magic.Length; i++) {
                str += $"{magic[i].MagicNumber}";

                if (i < magic.Length - 1)
                {
                    str += $",{Environment.NewLine}";
                } else
                {
                    str += $"{Environment.NewLine}";
                }
            }

            Console.WriteLine(str);
            Assert.Fail(str);
        }
    }
}