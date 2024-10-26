using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface.Factories;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmRunFieldViewModel : AlgorithmRunBaseViewModel
    {
        public AlgorithmRunFieldViewModel(IGraphAssemble<RunVertexModel> assemble,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger) : base(assemble)
        {
            messenger.Register<RunCreatedMessaged>(this, async (r, msg) => await OnRunCreated(r, msg));
        }

        private async Task OnRunCreated(object recipient, RunCreatedMessaged msg)
        {
            var graph = await CreateGraph(msg.Model);
            var range = msg.Model.GraphState.Range;
            Vertices = GetVerticesStates(msg.SubAlgorithms, range, graph);
            GraphState = graph;
        }
    }
}
