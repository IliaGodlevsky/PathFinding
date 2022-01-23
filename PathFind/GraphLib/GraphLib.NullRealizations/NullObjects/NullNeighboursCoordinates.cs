using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullNeighboursCoordinates
        : Singleton<NullNeighboursCoordinates>, INeighborhood
    {
        public IReadOnlyCollection<ICoordinate> Neighbours => Array.Empty<ICoordinate>();

        public INeighborhood Clone()
        {
            return Instance;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        private NullNeighboursCoordinates()
        {

        }
    }
}