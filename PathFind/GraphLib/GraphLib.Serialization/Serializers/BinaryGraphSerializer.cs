using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System.IO;
using System.Text;

namespace GraphLib.Serialization.Serializers
{
    public sealed class BinaryGraphSerializer : GraphSerializer
    {
        public BinaryGraphSerializer(IVertexFromInfoFactory converter,
            IGraphFactory graphFactory,
            IVertexCostFactory costFactory,
            ICoordinateFactory coordinateFactory) 
            : base(converter, graphFactory, costFactory, coordinateFactory)
        {
            
        }

        protected override GraphSerializationInfo LoadGraphInternal(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                return reader.ReadGraph(costFactory, coordinateFactory);                
            }
        }

        protected override void SaveGraphInternal(IGraph graph, Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.WriteGraph(graph);
            }
        }
    }
}
