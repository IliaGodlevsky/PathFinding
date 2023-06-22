using Newtonsoft.Json;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.Serialization;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System.IO;

namespace Pathfinding.App.Console.Extensions
{
    internal static class BinaryReaderExtensions
    {
        public static SerializationInfo ReadHistory(this BinaryReader reader, 
            SerializationInfo info, ICoordinateFactory factory)
        {
            int algorithms = reader.ReadInt32();
            for (int i = 0; i < algorithms; i++)
            {
                info.Obstacles.Add(reader.ReadCoordinates(factory));
                info.Visited.Add(reader.ReadCoordinates(factory));
                info.Ranges.Add(reader.ReadCoordinates(factory));
                info.Paths.Add(reader.ReadCoordinates(factory));
                info.Costs.Add(reader.ReadIntArray());
                var json = reader.ReadString();
                var statistics = JsonConvert.DeserializeObject<StatisticsModel>(json);
                info.Statistics.Add(statistics);
                info.Algorithms.Add(statistics.AlgorithmName);
            }
            return info;
        }
    }
}
