﻿using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;

namespace WindowsFormsVersion.Model
{
    internal sealed class VertexSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            return new Vertex(info);
        }
    }
}
