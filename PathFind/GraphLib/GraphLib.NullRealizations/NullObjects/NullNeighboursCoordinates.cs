using GraphLib.Interfaces;
using NullObject.Attributes;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullNeighboursCoordinates : INeighborhood
    {
        public static INeighborhood Instance => instance.Value;

        public IReadOnlyCollection<ICoordinate> Neighbours => new NullCoordinate[] { };

        public INeighborhood Clone()
        {
            return Instance;
        }

        private NullNeighboursCoordinates()
        {

        }

        private static readonly Lazy<INeighborhood> instance = new Lazy<INeighborhood>(() => new NullNeighboursCoordinates());
    }
}