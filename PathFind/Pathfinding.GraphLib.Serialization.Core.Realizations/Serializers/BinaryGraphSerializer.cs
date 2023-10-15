using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System.IO;
using System.Text;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers
{
    public sealed class BinaryGraphSerializer<TVertex> : GraphSerializer<TVertex>
        where TVertex : IVertex
    {
        public BinaryGraphSerializer(IVertexFromInfoFactory<TVertex> converter,
            IGraphFactory<TVertex> graphFactory,
            IVertexCostFactory costFactory,
            ICoordinateFactory coordinateFactory)
            : base(converter, graphFactory, costFactory, coordinateFactory)
        {

        }

        protected override GraphSerializationInfo DeserializeInternal(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                return reader.ReadGraph(costFactory, coordinateFactory);
            }
        }

        protected override void SerializeInternal(GraphSerializationInfo info, Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.WriteGraph(info);
            }
        }
    }
}
