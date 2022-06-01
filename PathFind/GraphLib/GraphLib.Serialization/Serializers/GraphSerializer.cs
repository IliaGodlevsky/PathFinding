using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace GraphLib.Serialization.Serializers
{
    public abstract class GraphSerializer : IGraphSerializer
    {
        protected readonly IVertexFromInfoFactory vertexFactory;
        protected readonly IGraphFactory graphFactory;
        protected readonly IVertexCostFactory costFactory;
        protected readonly ICoordinateFactory coordinateFactory;

        public GraphSerializer(IVertexFromInfoFactory converter,
            IGraphFactory graphFactory,
            IVertexCostFactory costFactory,
            ICoordinateFactory coordinateFactory)
        {
            this.vertexFactory = converter;
            this.graphFactory = graphFactory;
            this.costFactory = costFactory;
            this.coordinateFactory = coordinateFactory;
        }

        public IGraph LoadGraph(Stream stream)
        {
            try
            {
                var graphInfo = LoadGraphInternal(stream);
                var vertices = graphInfo.CreateVertices(vertexFactory).ToArray();
                BaseVertexCost.CostRange = graphInfo.CostRange;
                return graphFactory.CreateGraph(vertices, graphInfo.DimensionsSizes);
            }
            catch (Exception ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }

        public void SaveGraph(IGraph graph, Stream stream)
        {
            try
            {
                SaveGraphInternal(graph, stream);
            }
            catch (Exception ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }

        protected abstract GraphSerializationInfo LoadGraphInternal(Stream stream);

        protected abstract void SaveGraphInternal(IGraph graph, Stream stream);
    }
}
