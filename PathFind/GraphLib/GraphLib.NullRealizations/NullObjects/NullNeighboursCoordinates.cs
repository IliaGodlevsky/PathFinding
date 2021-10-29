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
        public static INeighboursCoordinates Instance => instance.Value;

        public IReadOnlyCollection<ICoordinate> Coordinates => new NullCoordinate[] { };

        public INeighboursCoordinates Clone()
        {
            return Instance;
        }

        private NullNeighboursCoordinates()
        {

        }

        private static readonly Lazy<INeighboursCoordinates> instance = new Lazy<INeighboursCoordinates>(() => new NullNeighboursCoordinates());
    }
}