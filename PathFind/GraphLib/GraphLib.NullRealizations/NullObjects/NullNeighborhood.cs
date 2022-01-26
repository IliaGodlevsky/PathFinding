using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullNeighborhood : Singleton<NullNeighborhood>, INeighborhood
    {
        public IReadOnlyCollection<ICoordinate> Neighbours => NullCoordinate.GetMany(0);

        public INeighborhood Clone()
        {
            return Instance;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        private NullNeighborhood()
        {

        }
    }
}