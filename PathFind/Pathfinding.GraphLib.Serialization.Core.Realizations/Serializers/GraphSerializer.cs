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
    public abstract class GraphSerializer<TVertex> : ISerializer<IGraph<TVertex>>
        where TVertex : IVertex
    {
        protected readonly IVertexFromInfoFactory<TVertex> vertexFactory;
        protected readonly IGraphFactory<TVertex> graphFactory;

        public GraphSerializer(IVertexFromInfoFactory<TVertex> factory,
            IGraphFactory<TVertex> graphFactory)
        {
            this.vertexFactory = factory;
            this.graphFactory = graphFactory;
        }

        public IGraph<TVertex> DeserializeFrom(Stream stream)
        {
            try
            {
                var graphInfo = DeserializeInternal(stream);
                var vertices = graphInfo.VerticesInfo
                    .Select(vertexFactory.CreateFrom)
                    .ToDictionary(item => item.Position);
                foreach (var info in graphInfo.VerticesInfo)
                {
                    var neighbours = info.Neighbourhood
                        .Select(coordinate => (IVertex)vertices[coordinate])
                        .ToList();
                    vertices[info.Position].Neighbours.AddRange(neighbours);
                }
                return graphFactory.CreateGraph(vertices.Values,
                    graphInfo.DimensionsSizes);
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(IGraph<TVertex> graph, Stream stream)
        {
            try
            {
                SerializeInternal(new((IGraph<IVertex>)graph), stream);
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        protected abstract GraphSerializationInfo DeserializeInternal(Stream stream);

        protected abstract void SerializeInternal(GraphSerializationInfo info, Stream stream);
    }
}
