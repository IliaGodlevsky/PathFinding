﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using Shared.Extensions;
using System;
using System.IO;
using System.Linq;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers
{
    public abstract class GraphSerializer<TGraph, TVertex> : ISerializer<TGraph>
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

        public TGraph DeserializeFrom(Stream stream)
        {
            try
            {
                var graphInfo = DeserializeInternal(stream);
                var vertices = graphInfo.VerticesInfo
                    .Select(vertexFactory.CreateFrom)
                    .ToDictionary(item => item.Position);
                foreach (var info in graphInfo.VerticesInfo)
                {
                    vertices[info.Position].Neighbours = info.Neighbourhood
                        .ToDictionary(item => (IVertex)vertices[item.Key], item => item.Value);
                }
                return graphFactory.CreateGraph(vertices.Values,
                    graphInfo.DimensionsSizes);
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(TGraph graph, Stream stream)
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
