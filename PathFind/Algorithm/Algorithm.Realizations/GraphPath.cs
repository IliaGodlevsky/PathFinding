using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations
{
    /// <summary>
    /// A class, that provides about path
    /// </summary>
    public sealed class GraphPath : IGraphPath
    {
        private IEnumerable<IVertex> Empty => Enumerable.Empty<IVertex>();

        public IEnumerable<IVertex> Path { get; }

        public GraphPath(IDictionary<ICoordinate, IVertex> parentVertices,
            IEndPoints endpoints) : this()
        {
            this.parentVertices = parentVertices;
            this.endpoints = endpoints;
            Path = TieEndPoints();
        }

        private GraphPath()
        {
            Path = Empty;
        }

        private IEnumerable<IVertex> TieEndPoints()
        {
            return CanTieEndPoints(parentVertices)
                ? UnwindVertices(endpoints.End)
                : Empty;
        }

        private IEnumerable<IVertex> UnwindVertices(IVertex start)
        {
            bool hasValue;
            var vertex = start;
            do
            {
                yield return vertex;
                hasValue = TryGetValue(vertex, out var value);
                vertex = value;
            }
            while (hasValue && !HasReachedTheEnd(vertex));
        }

        private bool CanTieEndPoints(IDictionary<ICoordinate, IVertex> parentVertices)
        {
            return parentVertices.Any() && TryGetValue(endpoints.End, out _);
        }

        private bool HasReachedTheEnd(IVertex vertex)
        {
            if (TryGetValue(vertex, out var parentVertex))
            {
                bool isNeighbourOf = vertex.IsNeighbourOf(parentVertex);
                return IsStartOfPath(vertex) || !isNeighbourOf;
            }

            return true;
        }

        private bool TryGetValue(IVertex vertex, out IVertex value)
        {
            var position = vertex.Position;
            return parentVertices.TryGetValue(position, out value);
        }

        private bool IsStartOfPath(IVertex vertex)
        {
            bool isStartVertex = vertex.IsEqual(endpoints.Start);
            return isStartVertex || vertex.IsDefault();
        }

        private readonly IEndPoints endpoints;
        private readonly IDictionary<ICoordinate, IVertex> parentVertices;
    }
}
