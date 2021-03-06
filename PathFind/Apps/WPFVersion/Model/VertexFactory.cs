﻿using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace WPFVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory
    {
        public IVertex CreateVertex(INeighboursCoordinates coordinateRadar, ICoordinate coordinate)
        {
            return new Vertex(coordinateRadar, coordinate);
        }
    }
}
