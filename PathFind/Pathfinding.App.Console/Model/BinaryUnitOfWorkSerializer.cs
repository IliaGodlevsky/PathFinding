using Newtonsoft.Json;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System;
using System.IO;
using System.Text;

namespace Pathfinding.App.Console.Model
{
    internal sealed class BinaryUnitOfWorkSerializer : ISerializer<IUnitOfWork>
    {
        private readonly ICoordinateFactory factory;
        private readonly JsonSerializerSettings settings;

        public BinaryUnitOfWorkSerializer(ICoordinateFactory factory)
        {
            this.factory = factory;
            this.settings = GetSettings();
        }

        public IUnitOfWork DeserializeFrom(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                var unitOfWork = new MemoryUnitOfWork();
                int keyCount = reader.ReadInt32();
                for (int i = 0; i < keyCount; i++)
                {
                    string keyString = reader.ReadString();
                    Guid key = Guid.Parse(keyString);
                    unitOfWork.Keys.Add(key);
                    unitOfWork.ObstacleRepository.Add(key, reader.ReadCoordinates(factory));
                    unitOfWork.VisitedRepository.Add(key, reader.ReadCoordinates(factory));
                    unitOfWork.RangeRepository.Add(key, reader.ReadCoordinates(factory));
                    unitOfWork.PathRepository.Add(key, reader.ReadCoordinates(factory));
                    unitOfWork.CostRepository.Add(key, reader.ReadIntArray());
                    var json = reader.ReadString();
                    var statistics = JsonConvert.DeserializeObject<StatisticsNote>(json, settings);
                    unitOfWork.StatisticsRepository.Add(key, statistics);
                }
                return unitOfWork;
            }
        }

        public void SerializeTo(IUnitOfWork item, Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.Write(item.Keys.Count);
                foreach (var key in item.Keys)
                {                    
                    writer.Write(key.ToString());
                    writer.WriterCoordinates(item.ObstacleRepository.Get(key));
                    writer.WriterCoordinates(item.VisitedRepository.Get(key));
                    writer.WriterCoordinates(item.RangeRepository.Get(key));
                    writer.WriterCoordinates(item.PathRepository.Get(key));
                    writer.WriteIntArray(item.CostRepository.Get(key));
                    var statistics = item.StatisticsRepository.Get(key);
                    var json = JsonConvert.SerializeObject(statistics, settings);
                    writer.Write(json);
                }
            }
        }

        private JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
        }
    }
}
