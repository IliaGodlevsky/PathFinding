using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmRunFieldViewModel : AlgorithmRunBaseViewModel
    {
        public AlgorithmRunFieldViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger)
            : base(messenger)
        {
            messenger.Register<RunCreatedMessaged>(this, OnRunCreated);
        }

        private void OnRunCreated(object recipient, RunCreatedMessaged msg)
        {
            var graph = CreateGraph();
            var rangeMsg = new QueryPathfindingRangeMessage();
            messenger.Send(rangeMsg);
            var range = rangeMsg.PathfindingRange;
            Vertices = GetVerticesStates(msg.SubAlgorithms, range, graph);
            Processed = new(Vertices.Reverse());
            GraphState = graph.Values;
        }
    }
}
