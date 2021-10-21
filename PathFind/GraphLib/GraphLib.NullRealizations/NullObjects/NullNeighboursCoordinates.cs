using GraphLib.Interfaces;
using NullObject.Attributes;
using System;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullNeighboursCoordinates : INeighboursCoordinates
    {
        public ICoordinate[] Coordinates => new NullCoordinate[] { };

        public INeighboursCoordinates Clone()
        {
            return new NullNeighboursCoordinates();
        }
    }
}