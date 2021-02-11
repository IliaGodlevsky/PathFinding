using GraphLib.Extensions;
using GraphLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Infrastructure
{
    /// <summary>
    /// A class, that provides pathfinding information from <see cref="IGraph"/>
    /// </summary>
    public sealed class GraphPath : IGraphPath
    {
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
            Path = new IVertex[] { };
        }

        private IEnumerable<IVertex> TieEndPoints()
        {
            if (CanTieEndPoints(parentVertices))
            {
                var temp = endpoints.End;
                yield return temp;
                while (!IsStartOfPath(temp))
                {
                    temp = parentVertices[temp.Position];
                    yield return temp;
                }
            }
        }

        private bool CanTieEndPoints(IDictionary<ICoordinate, IVertex> parentVertices)
        {
            return parentVertices.Any()
                && parentVertices.TryGetValue(endpoints.End.Position, out _);
        }

        private bool IsStartOfPath(IVertex vertex)
        {
            return vertex.IsEqual(endpoints.Start) || vertex.IsDefault;
        }

        private readonly IEndPoints endpoints;
        private readonly IDictionary<ICoordinate, IVertex> parentVertices;
    }
}
