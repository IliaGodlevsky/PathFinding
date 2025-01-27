using Pathfinding.App.Console.Model;
using Pathfinding.Service.Interface.Models.Read;
using System.Reactive;

namespace Pathfinding.App.Console.Messages.ViewModel
{
    internal sealed class AsyncGraphActivatedMessage : IAsyncMessage<Unit>
    {
        public GraphModel<GraphVertexModel> Graph { get; }

        public Action<Unit> Signal { get; set; } = unit => throw new InvalidOperationException();

        public AsyncGraphActivatedMessage(GraphModel<GraphVertexModel> graph)
        {
            Graph = graph;
        }
    }
}
