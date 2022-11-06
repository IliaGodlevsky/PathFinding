using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface IVertexFactory<out TVertex>
        where TVertex : IVertex
    {
        TVertex CreateVertex(ICoordinate coordinate);
    }
}
