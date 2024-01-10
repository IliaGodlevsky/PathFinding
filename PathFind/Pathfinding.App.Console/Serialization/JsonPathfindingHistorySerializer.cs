using AutoMapper;
using Newtonsoft.Json;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class JsonPathfindingHistorySerializer : ISerializer<IEnumerable<PathfindingHistorySerializationDto>>
    {
        private readonly IMapper mapper;

        public JsonPathfindingHistorySerializer(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<PathfindingHistorySerializationDto> DeserializeFrom(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.Default, false, 1024, leaveOpen: true))
            {
                string deserialized = reader.ReadToEnd();
                var dtos = JsonConvert.DeserializeObject<PathfindingHistoryJsonSerializationDto[]>(deserialized);
                var history = mapper.Map<PathfindingHistorySerializationDto[]>(dtos);
                return history;
            }
        }

        public void SerializeTo(IEnumerable<PathfindingHistorySerializationDto> item, Stream stream)
        {
            var jsonDtos = mapper.Map<PathfindingHistoryJsonSerializationDto[]>(item);
            string serialized = JsonConvert.SerializeObject(jsonDtos);
            using (var writer = new StreamWriter(stream, Encoding.Default, 1024, leaveOpen: true))
            {
                writer.Write(serialized);
            }
        }
    }
}
