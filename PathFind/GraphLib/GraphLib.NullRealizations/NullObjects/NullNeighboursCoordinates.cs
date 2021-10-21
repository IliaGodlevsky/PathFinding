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
            Coordinates = new NullCoordinate[] { new NullCoordinate() };
        }

        public ICoordinate[] Coordinates { get; }

        public INeighboursCoordinates Clone()
        {
            return new NullNeighboursCoordinates();
        }
    }
}