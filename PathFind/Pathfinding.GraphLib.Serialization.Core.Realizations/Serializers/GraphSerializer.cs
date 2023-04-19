using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using Shared.Extensions;
using System;
using System.IO;
using System.Linq;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers
{
    public abstract class GraphSerializer<TGraph, TVertex> : IGraphSerializer<TGraph, TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        protected readonly IVertexFromInfoFactory<TVertex> vertexFactory;
        protected readonly IGraphFactory<TGraph, TVertex> graphFactory;
        protected readonly IVertexCostFactory costFactory;
        protected readonly ICoordinateFactory coordinateFactory;

        public GraphSerializer(IVertexFromInfoFactory<TVertex> factory,
            IGraphFactory<TGraph, TVertex> graphFactory,
            IVertexCostFactory costFactory,
            ICoordinateFactory coordinateFactory)
        {
            this.vertexFactory = factory;
            this.graphFactory = graphFactory;
            this.costFactory = costFactory;
            this.coordinateFactory = coordinateFactory;
        }

        public TGraph LoadGraph(Stream stream)
        {
            try
            {
                var graphInfo = LoadGraphInternal(stream);
                var vertices = graphInfo.VerticesInfo
                    .Select(vertexFactory.CreateFrom)
                    .ToDictionary(item => item.Position);
                foreach (var info in graphInfo.VerticesInfo)
                {
                    var neighbours = info.Neighbourhood
                        .Select(coordinate => (IVertex)vertices[coordinate])
                        .ToList();
                    vertices[info.Position].Neighbours = neighbours;
                }
                return graphFactory.CreateGraph(vertices.Values, 
                    graphInfo.DimensionsSizes);
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(ex.Message, ex);
            }
        }

        public void SaveGraph(IGraph<IVertex> graph, Stream stream)
        {
            try
            {
                SaveGraphInternal(new(graph), stream);
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(ex.Message, ex);
            }
        }

        protected abstract GraphSerializationInfo LoadGraphInternal(Stream stream);

        protected abstract void SaveGraphInternal(GraphSerializationInfo info, Stream stream);
    }
}
