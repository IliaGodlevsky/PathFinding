using GraphLib.Interfaces;
using NullObject.Attributes;
using System;

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

        public ICoordinate[] Coordinates => coordinates;

        private readonly NullCoordinate[] coordinates;

        public INeighboursCoordinates Clone()
        {
            return new NullNeighboursCoordinates();
        }
    }
}