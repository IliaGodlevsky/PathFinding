using GraphLib.Interfaces;
using NullObject.Attributes;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullNeighboursCoordinates : INeighboursCoordinates
    {
        public NullNeighboursCoordinates()
        {
            coordinates = new NullCoordinate[] { new NullCoordinate() };
        }

        public IEnumerable<ICoordinate> Coordinates => coordinates;

        private readonly NullCoordinate[] coordinates;
    }
}