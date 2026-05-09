using System.Reflection;
using ChessEngine;
using ChessEngine.Evaluators;
using ChessEngine.SearchUtils;

MagicBishop.Initialize(MagicNumbers.BishopMagicNumbers);
MagicRook.Initialize(MagicNumbers.RookMagicNumbers);

var debugPath = Path.Join(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location), "debug.txt");
var evaluator = new DepthSearchEvaluator(new PeiceCounterEvaluator());

if (bool.TryParse(Environment.GetEnvironmentVariable("UCI_DEBUG_RUN_ENABLED"), out bool e) && e)
{
    var engine = new Engine(evaluator, debugMode: true);
    var uciEngineWrapper = new UciEngineWrapper(engine, debugPath);
    uciEngineWrapper.InitiateDebugRun();
} else
{
    var engine = new Engine(evaluator);
    var uciEngineWrapper = new UciEngineWrapper(engine, debugPath);
    uciEngineWrapper.Initiate();
}