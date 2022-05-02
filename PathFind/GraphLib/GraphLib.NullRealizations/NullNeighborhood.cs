using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphLib.NullRealizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullNeighborhood : Singleton<NullNeighborhood, INeighborhood>, INeighborhood
    {
        public IReadOnlyCollection<ICoordinate> Neighbours => NullCoordinate.GetMany(0);

        private NullNeighborhood()
        {

        }
    }
}