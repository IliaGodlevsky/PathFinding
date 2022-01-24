using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    public sealed class NullGraph : Singleton<NullGraph>, IGraph
    {
        public IVertex this[ICoordinate position]
        {
            get => NullVertex.Instance;
        }

        public int[] DimensionsSizes => Array.Empty<int>();

        public IReadOnlyCollection<IVertex> Vertices => Array.Empty<NullVertex>();

        public int Size => 0;

        public override bool Equals(object obj)
        {
            return obj is NullGraph;
        }

        public override string ToString()
        {
            return string.Empty;
        }

        public override int GetHashCode()
        {
            return default;
        }

        public IGraph Clone()
        {
            return NullGraph.Instance;
        }

        private NullGraph()
        {

        }
    }
}
