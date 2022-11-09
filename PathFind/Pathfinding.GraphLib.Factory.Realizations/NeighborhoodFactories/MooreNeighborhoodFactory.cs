using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Neighborhoods;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories
{
    public sealed class MooreNeighborhoodFactory : INeighborhoodFactory
    {
        public INeighborhood CreateNeighborhood(ICoordinate coordinate)
        {
            return new MooreNeighborhood(coordinate);
        }
    }
}