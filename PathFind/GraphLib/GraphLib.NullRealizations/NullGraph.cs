using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphLib.NullRealizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullGraph : Singleton<NullGraph, IGraph>, IGraph
    {
        public int[] DimensionsSizes => Array.Empty<int>();

        public IReadOnlyCollection<IVertex> Vertices => NullVertex.GetMany(0);

        public int Size => 0;

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

        public override string ToString()
        {
            return string.Empty;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
