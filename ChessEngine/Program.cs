using System.Reflection;
using ChessEngine;

var debugPath = Path.Join(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location), "debug.txt");

if (bool.TryParse(Environment.GetEnvironmentVariable("UCI_DEBUG_RUN_ENABLED"), out bool e) && e)
{
    var engine = new RandomEngine(debugMode: true);
    var uciEngineWrapper = new UciEngineWrapper(engine, debugPath);
    uciEngineWrapper.InitiateDebugRun();
} else
{
    var engine = new RandomEngine();
    var uciEngineWrapper = new UciEngineWrapper(engine, debugPath);
    uciEngineWrapper.Initiate();
}