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
    public sealed class NullVertex : Singleton<NullVertex, IVertex>, IVertex, IEquatable<IVertex>
    {
        public bool IsObstacle
        {
            get => true;
            set { }
        }

        public IVertexCost Cost
        {
            get => NullCost.Instance;
            set { }
        }

        public IReadOnlyCollection<IVertex> Neighbours => GetMany(0);

        public IGraph Graph => NullGraph.Instance;

        public ICoordinate Position
        {
            get => NullCoordinate.Instance;
            set { }
        }

        public INeighborhood Neighborhood => NullNeighborhood.Instance;

        private NullVertex()
        {

        }

        public bool Equals(IVertex other)
        {
            return other is NullVertex;
        }
    }
}