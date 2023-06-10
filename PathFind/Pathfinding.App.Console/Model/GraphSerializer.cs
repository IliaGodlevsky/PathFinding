using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.App.Console.Model
{
    internal sealed class GraphSerializer : ISerializer<SerializationInfo>
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> rangeSerializer;
        private readonly ISerializer<IUnitOfWork> unitOfWorkSerializer;
        private readonly ISerializer<Graph2D<Vertex>> graphSerializer;

        public GraphSerializer(ISerializer<IEnumerable<ICoordinate>> rangeSerializer, 
            ISerializer<IUnitOfWork> unitOfWorkSerializer, 
            ISerializer<Graph2D<Vertex>> graphSerializer)
        {
            this.rangeSerializer = rangeSerializer;
            this.unitOfWorkSerializer = unitOfWorkSerializer;
            this.graphSerializer = graphSerializer;
        }

        public SerializationInfo DeserializeFrom(Stream stream)
        {
            try
            {
                var graph = graphSerializer.DeserializeFrom(stream);
                var range = rangeSerializer.DeserializeFrom(stream);
                var unit = unitOfWorkSerializer.DeserializeFrom(stream);
                return new SerializationInfo { Graph = graph, Range = range, UnitOfWork = unit };
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(SerializationInfo item, Stream stream)
        {
            try
            {
                graphSerializer.SerializeTo(item.Graph, stream);
                rangeSerializer.SerializeTo(item.Range, stream);
                unitOfWorkSerializer.SerializeTo(item.UnitOfWork, stream);
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}
