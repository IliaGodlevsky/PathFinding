using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmRunFieldViewModel : AlgorithmRunBaseViewModel
    {
        public AlgorithmRunFieldViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger)
            : base(messenger)
        {
            messenger.Register<RunCreatedMessaged>(this, async (r, msg) => await OnRunCreated(r, msg));
        }

        private async Task OnRunCreated(object recipient, RunCreatedMessaged msg)
        {
            var graph = CreateGraph();
            var rangeMsg = new QueryPathfindingRangeMessage();
            messenger.Send(rangeMsg);
            var range = rangeMsg.PathfindingRange;
            Vertices = await Task.Run(() => GetVerticesStates(msg.SubAlgorithms, range, graph));
            GraphState = graph.Values;
        }
    }
}
