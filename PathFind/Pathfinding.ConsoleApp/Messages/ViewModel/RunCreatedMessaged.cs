using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class RunCreatedMessaged
    {
        public AlgorithmRunHistoryModel Model { get; }

        public RunCreatedMessaged(AlgorithmRunHistoryModel model)
        {
            Model = model;
        }
    }
}
