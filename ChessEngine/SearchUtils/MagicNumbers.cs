using ChessEngine.Utils;

namespace ChessEngine.SearchUtils
{
    public static class MagicNumbers
    {
        private static readonly Random random = new Random();
        public static (ulong MagicNumber, ulong[] MoveBoards) GenMagicMoveArr(ulong blockerMask, Dictionary<ulong, ulong> blockerMoveBoardMap, ulong? num = null)
        {
            ulong magicNum = num == null 
                ? random.NextULong() & random.NextULong() & random.NextULong() // Magic numbers tend to be sparse
                : num.Value;

            while(true)
            {
                bool isMagic = true;

                int bits = blockerMask.GetNumSetBits();
                var moveBoards = new ulong[(int)Math.Pow(2, bits)];

                // Since zero is a valid move board we first set it to a board no piece could have
                // So we know if it has been set or not
                Array.Fill(moveBoards, CommonBitBoards.AllTiles);

                foreach(var kvp in blockerMoveBoardMap)
                {
                    var blocker = kvp.Key;
                    var moves = kvp.Value;

                    var index = (blocker * magicNum) >> (64 - bits);
                    
                    // Unset => set for this index
                    if (moveBoards[index] == CommonBitBoards.AllTiles)
                    {
                        moveBoards[index] = moves;
                    } 
                    
                    // If already set for a different blocker board BUT the move boards match then that is okay
                    // Both blockers will map to this index
                    else if (moveBoards[index] == moves)
                    {
                        continue;
                    }

                    // 2 blocker boards with different move boards point to the same index
                    // therefore the random number is not magic and we must try another
                    else
                    {
                        isMagic = false;
                        magicNum = random.NextULong() & random.NextULong() & random.NextULong();
                        break;
                    }
                }

                if (isMagic)
                {
                    return (magicNum, moveBoards);
                }
            }
        }

        public static List<ulong> GenBlockerBoards(ulong blockerMask)
        {
            List<ulong> bits = [];

            while(blockerMask != 0)
            {
                bits.Add(blockerMask.GetLsb());
                blockerMask = blockerMask.PopLsb();
            }

            List<ulong> blockerBoards = [0UL];
            foreach(var bit in bits)
            {
                int existingCnt = blockerBoards.Count;

                for (var i = 0; i < existingCnt; i++)
                {
                    blockerBoards.Add(blockerBoards[i] | bit);
                }
            }

            return blockerBoards;

        }
        public static readonly ulong[] RookMagicNumbers = [
            36029347848790160,
            594475219536584704,
            11565279029608054792,
            360292509978996736,
            144119620754079776,
            144132798112942592,
            36074976515719424,
            108087769741533312,
            2310487348512571392,
            6917599408290795520,
            11601413515034756230,
            1503639368542261280,
            1225260676702023936,
            5918011419803255040,
            41658851174912004,
            2306124527278531840,
            684565010434752576,
            3188690923470995460,
            1144592695074816,
            1157425654258991236,
            4613938368131367041,
            73183768856248576,
            4400211821160,
            288232575448383617,
            73113131643068554,
            292738666957119488,
            35485020881408,
            40532469662941768,
            2884557761431995392,
            281505041743880,
            289637755330363396,
            1341687654482180,
            36099167919014002,
            2305993917193003014,
            27307678103511040,
            11552577505122586624,
            43982621000704,
            18577365676329104,
            18577369971295240,
            454869130823073860,
            36453209732153376,
            9511637666102394884,
            4908924694421372945,
            9223460002633809928,
            36310306356264976,
            563517157670920,
            576752191738740786,
            594757180113158148,
            4936262400773980288,
            9232415258277184000,
            633392249242368,
            4037529846792980736,
            1153497650854101120,
            434742500146577536,
            140887812341888,
            1100661131776,
            5188428529200533538,
            15006012668350308481,
            11547256245211631873,
            145204271122593,
            11538785212725469194,
            4611967502061144081,
            5911558895044868,
            72656279220323330
        ];

        public static readonly ulong[] BishopMagicNumbers = [
            577050125031277026,
            2269409185120516,
            22520755510124712,
            582129853583593504,
            9368895974254313472,
            1153662586214940736,
            9223390180948312064,
            9223374235945189376,
            9367523371409081152,
            1153155735044227108,
            5775903907981066752,
            290491135271485440,
            4611690488482799108,
            4508066528886888,
            288230515877093376,
            72709502402690,
            290483550625563145,
            4789610123231296,
            563242212529664,
            4612813053283534848,
            577024544372555776,
            577023986816255296,
            3476826208526733312,
            9799975867407990848,
            13988329980565129220,
            2254755423327520,
            2316134438721044497,
            36592297130885123,
            1407447914790916,
            4611837751309372424,
            9273475132534335490,
            4612040894546247762,
            37156909375038464,
            12123848870153421906,
            2308112779196761472,
            5773693921494436352,
            577621879532036224,
            14421111230495260832,
            2817000351482885,
            289466245508505857,
            16465762774468133896,
            9800961072839004168,
            1153239814565273664,
            153122670840383489,
            9301068068258579456,
            9241395235765358848,
            146375818489168018,
            757880207395783936,
            599550501050646532,
            1155737909094023168,
            144120686728970240,
            2314991223183769616,
            306526267644313794,
            579029289346924544,
            1170976039753614338,
            4505807308338209,
            4629701550825678976,
            7134335283073523784,
            72057598416945682,
            2305851019332158211,
            76143596274208,
            7079658648858857984,
            4611782810365330432,
            18031993002590528
        ];
    }
}