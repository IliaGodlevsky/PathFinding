using Pathfinding.ConsoleApp.Model;
using Pathfinding.Service.Interface.Models.Read;
using System;
using System.Reactive;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
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
