using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class LayerMessage(ILayer layer)
    {
        public ILayer Layer { get; } = layer;
    }
}
