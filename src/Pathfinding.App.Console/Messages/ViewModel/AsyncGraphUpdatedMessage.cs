using Pathfinding.Service.Interface.Models.Read;
using System.Reactive;

namespace Pathfinding.App.Console.Messages.ViewModel
{
    internal sealed class AsyncGraphUpdatedMessage : IAsyncMessage<Unit>
    {
        public GraphInformationModel Model { get; }

        public Action<Unit> Signal { get; set; } = unit => throw new InvalidOperationException();

        public AsyncGraphUpdatedMessage(GraphInformationModel model)
        {
            Model = model;
        }
    }
}
