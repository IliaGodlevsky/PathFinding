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
            get => NullCost.Interface;
            set { }
        }

        public IReadOnlyCollection<IVertex> Neighbours
        {
            get => GetMany(0);
            set { }
        }

        public ICoordinate Position
        {
            get => NullCoordinate.Interface;
            set { }
        }

        private NullVertex()
        {

        }

        public bool Equals(IVertex other)
        {
            return other is NullVertex;
        }
    }
}