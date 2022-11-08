using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public interface IVertexFromInfoFactory<out TVertex>
        where TVertex : IVertex
    {
        TVertex CreateFrom(VertexSerializationInfo info);
    }
}
