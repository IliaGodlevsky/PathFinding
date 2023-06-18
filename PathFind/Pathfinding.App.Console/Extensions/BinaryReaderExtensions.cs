using Newtonsoft.Json;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System;
using System.IO;

namespace Pathfinding.App.Console.Extensions
{
    internal static class BinaryReaderExtensions
    {
        public static IPathfindingHistory ReadHistory(this BinaryReader reader, ICoordinateFactory factory)
        {
            var history = new PathfindingHistory();
            int keyCount = reader.ReadInt32();
            for (int i = 0; i < keyCount; i++)
            {
                var key = new Guid(reader.ReadBytes(16));
                history.Algorithms.Add(key);
                history.ObstacleVertices.Add(key, reader.ReadCoordinates(factory));
                history.VisitedVertices.Add(key, reader.ReadCoordinates(factory));
                history.RangeVertices.Add(key, reader.ReadCoordinates(factory));
                history.PathVertices.Add(key, reader.ReadCoordinates(factory));
                history.Costs.Add(key, reader.ReadIntArray());
                var json = reader.ReadString();
                var statistics = JsonConvert.DeserializeObject<Statistics>(json);
                history.Statistics.Add(key, statistics);
            }
            return history;
        }
    }
}
