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
        public IReadOnlyCollection<ICoordinate> Coordinates => new NullCoordinate[] { };

        public INeighboursCoordinates Clone()
        {
            return new NullNeighboursCoordinates();
        }
    }
}