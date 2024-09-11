using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class RunHistoriesUploadedMessage
    {
        public AlgorithmRunHistoryModel[] Runs { get; }

        public RunHistoriesUploadedMessage(AlgorithmRunHistoryModel[] runs)
        {
            Runs = runs;
        }
    }
}
