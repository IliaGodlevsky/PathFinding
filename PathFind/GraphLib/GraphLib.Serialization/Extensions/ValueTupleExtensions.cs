using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;

namespace GraphLib.Serialization.Extensions
{
    internal static class ValueTupleExtensions
    {
        public static IGraph Create(this (IGraphFactory GraphFactory, IVertexFromInfoFactory VertexFactory) factories, GraphSerializationInfo info)
        {
            var vertices = factories.VertexFactory.CreateManyFrom(info.VerticesInfo);
            return factories.GraphFactory.CreateGraph(vertices, info.DimensionsSizes);
        }
    }
}
