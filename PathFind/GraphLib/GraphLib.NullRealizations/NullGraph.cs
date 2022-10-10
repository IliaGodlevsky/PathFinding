using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GraphLib.NullRealizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullGraph : Singleton<NullGraph, IGraph>, IGraph
    {
        public IReadOnlyList<int> DimensionsSizes => Array.Empty<int>();

        public int Count => 0;

        private NullGraph()
        {

        }

        public IVertex Get(ICoordinate coordinate)
        {
            return NullVertex.Interface;
        }

        public override bool Equals(object obj)
        {
            return obj is NullGraph;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            return Enumerable.Empty<IVertex>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
