using GraphLib.Interfaces;
using NullObject.Attributes;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    public sealed class NullVertex : IVertex, IEquatable<IVertex>
    {
        public static IVertex Instance => instance.Value;

        public bool IsObstacle { get => true; set { } }
        public IVertexCost Cost { get => NullCost.Instance; set { } }
        public IReadOnlyCollection<IVertex> Neighbours { get; }
        public IGraph Graph { get; }
        public ICoordinate Position { get => NullCoordinate.Instance; set { } }
        public bool Equals(IVertex other) => other is NullVertex;
        public INeighborhood NeighboursCoordinates => NullNeighboursCoordinates.Instance;

        private NullVertex()
        {
            Neighbours = new NullVertex[] { };
            Graph = NullGraph.Instance;
        }

        private static readonly Lazy<IVertex> instance = new Lazy<IVertex>(() => new NullVertex());
    }
}