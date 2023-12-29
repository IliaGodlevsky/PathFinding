using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class BinaryIntArraySerializer : ISerializer<IEnumerable<int>>
    {
        public IEnumerable<int> DeserializeFrom(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                return reader.ReadIntArray();
            }
        }

        public void SerializeTo(IEnumerable<int> item, Stream stream)
        {
            using(var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.WriteIntArray(item.ToReadOnly());
            }
        }
    }
}
