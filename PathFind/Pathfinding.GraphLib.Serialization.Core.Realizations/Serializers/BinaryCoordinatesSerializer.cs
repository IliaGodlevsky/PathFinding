﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers
{
    public sealed class BinaryCoordinatesSerializer : ISerializer<IEnumerable<ICoordinate>>
    {
        public IEnumerable<ICoordinate> DeserializeFrom(Stream stream)
        {
            try
            {
                using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
                {
                    return reader.ReadCoordinates();
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }

        public void SerializeTo(IEnumerable<ICoordinate> item, Stream stream)
        {
            try
            {
                using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
                {
                    writer.WriteCoordinates(item.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException(ex.Message, ex);
            }
        }
    }
}
