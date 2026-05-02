using System.Net.Quic;
using System.Reflection;
using ChessEngine.Uci;

namespace ChessEngine
{
    public class UciEngineWrapper
    {
        private IUciEngine Engine {get; set;}
        /// <summary>
        /// True if GUI sends UCI debug on message
        /// </summary>
        private bool isUciDebug = false;
        /// <summary>
        /// True if running through debug file.
        /// Don't write to debug file here
        /// </summary>
        private bool writeToDebugTxtFile = true;
        private string debugPath;

        public UciEngineWrapper(IUciEngine engine, string debugPath)
        {
            this.Engine = engine;
            this.debugPath = debugPath;
        }



        public void Initiate() {

            string? line;

            if (File.Exists(debugPath))
            {
                File.Delete(debugPath);
            }

            while ((line = Console.ReadLine()) != null)
            {
                var quit = HandleIncomingMessage(line);

                if (quit)
                    break;
            }
        }

        public void InitiateDebugRun() {

            writeToDebugTxtFile = false;
            var lines = File.ReadAllLines(debugPath);

            foreach(string line in lines)
            {
                var quit = HandleIncomingMessage(line);

                if (quit)
                    break;
            }
        }

        private bool HandleIncomingMessage(string line)
        {
            var quit = false;
            try
            {
                if (writeToDebugTxtFile)
                {
                    File.AppendAllText(debugPath, line + Environment.NewLine);
                }

                SendDebugMessage($"Received Command: \"{line}\"");
                line = line.Trim().ToLower();
                bool canParse = Enum.TryParse<UciGuiCommand>(line.Split(" ").First(), true, out var cmd);
                if (!canParse)
                {
                    HandleUnknownMessage(line);
                    return quit;
                }

                switch(cmd)
                {
                    case UciGuiCommand.Uci:
                        HandleUciMessage(line);
                        break;
                    case UciGuiCommand.Debug:
                        HandleDebugMessage(line);
                        break;
                    case UciGuiCommand.IsReady:
                        HandleIsReadyMessage(line);
                        break;
                        case UciGuiCommand.UciNewGame:
                        HandleUciNewGameMessage(line);
                        break;
                    case UciGuiCommand.Position:
                        HandlePositionMessage(line);
                        break;
                    case UciGuiCommand.Go:
                        HandleGoMessage(line);
                        break;
                    case UciGuiCommand.Quit:
                        quit = true;
                        break;
                    default:
                        HandleUnknownMessage(line);
                        break;
                }
            } catch(Exception ex)
            {
                SendDebugMessage($"Exception encountered {ex}");
            }

            return quit;
        }

        public void SendDebugMessage(string msg)
        {
            if (isUciDebug)
            {
                Console.WriteLine($"{Enum.GetName(UciEngineCommand.Info)!.ToLower()} DEBUG: \"{msg}\"");
            }
        }

        public void SendMessage(UciEngineCommand cmd, string? msg = null)
        {
            string c = string.IsNullOrEmpty(msg)
                ? Enum.GetName(cmd)!.ToLower()
                : $"{Enum.GetName(cmd)!.ToLower()} {msg}";

            SendDebugMessage($"Sending Command: \"{c}\"");

            Console.WriteLine(c);
        }

        public void HandleUciMessage(string msg)
        {
            SendMessage(UciEngineCommand.Id, $"name {Engine.Name}");
            SendMessage(UciEngineCommand.Id, $"author {Engine.Author}");

            // Send options here

            SendMessage(UciEngineCommand.UciOk);
        }

        public void HandleDebugMessage(string msg)
        {
            isUciDebug = msg.Split(" ")[1] == "on";
            SendDebugMessage("Debugging is turned on");
        }

        public void HandleIsReadyMessage(string msg)
        {
            // Setup any heavy internal paramaters here
            SendMessage(UciEngineCommand.ReadyOk);
        }

        public void HandleUciNewGameMessage(string msg)
        {
            // Nothing to do here
        }

        public void HandlePositionMessage(string msg)
        {
            var words = msg.Split(" ");

            if (words[1] == "fen")
            {
                throw new NotImplementedException("Fen parsing not implemented");
            }

            var moveWordIndex = words.IndexOf("moves");
            var moves = moveWordIndex == -1
                ? []
                : words[(moveWordIndex + 1)..];

            Engine.UpdateBoard(moves);
        }

        public void HandleGoMessage(string msg)
        {
            var coords = Engine.GetBestMoveCoordinates();
            SendMessage(UciEngineCommand.BestMove, coords);
        }

        public void HandleUnknownMessage(string msg)
        {
            SendDebugMessage($"Unknown Message Received: \"{msg}\"");
        }
    }
}