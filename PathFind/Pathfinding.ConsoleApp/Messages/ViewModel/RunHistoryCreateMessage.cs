using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class RunHistoryCreateMessage
    {
        public AlgorithmRunHistoryModel Run { get; }

        public RunHistoryCreateMessage(AlgorithmRunHistoryModel run)
        {
            Run = run;
        }
    }
}
