using Pathfinding.Domain.Interface.Factories;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class NeighbourhoodMessage
    {
        public INeighborhoodFactory Factory { get; }

        public NeighbourhoodMessage(INeighborhoodFactory factory)
        {
            Factory = factory;
        }
    }
}
