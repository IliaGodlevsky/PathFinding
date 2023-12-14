using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.Extensions;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class BinaryAlgorithmSerializer : ISerializer<IEnumerable<AlgorithmSerializationDto>>
    {
        public IEnumerable<AlgorithmSerializationDto> DeserializeFrom(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                return reader.ReadAlgorithm();
            }
        }

        public void SerializeTo(IEnumerable<AlgorithmSerializationDto> algorithms, Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.WriteHistory(Array.AsReadOnly(algorithms.ToArray()));
            }
        }
    }
}
