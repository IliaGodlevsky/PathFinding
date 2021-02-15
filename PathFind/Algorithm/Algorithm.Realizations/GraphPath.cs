using Algorithm.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations
{
    /// <summary>
    /// A class, that provides about path
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
                do
                {
                    yield return temp;
                    temp = parentVertices[temp.Position];
                } while (!IsStartOfPath(temp));

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
