using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Infrastructure
{
    /// <summary>
    /// A class, that provides pathfinding information from <see cref="IGraph"/>
    /// </summary>
    public sealed class GraphPath
    {
        /// <summary>
        /// Returns extracted path
        /// </summary>
        public IEnumerable<IVertex> Path { get; private set; }

        /// <summary>
        /// Shows whether the path was extracted from the graph
        /// </summary>
        public bool IsExtracted { get; private set; }

        /// <summary>
        /// Returns the number of vertices in the path
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Returns the sum of costs of the vertices in the path
        /// </summary>
        public int Cost { get; private set; }

        public IDictionary<ICoordinate, IVertex> ParentVertices { get; set; }

        public IVertex Start { get; set; }

        public IVertex End { get; set; }

        public GraphPath(IDictionary<ICoordinate, IVertex> parentVertices,  
            IVertex start, IVertex end) : this()
        {
            ParentVertices = parentVertices;
            Start = start;
            End = end;
            TryExtractPath();
        }

        public void HighlightPath()
        {
            Path.Where(vertex => !vertex.IsEqual(End) && !vertex.IsEqual(Start))
                .ForEach(vertex => vertex.MarkAsPath());
        }

        /// <summary>
        /// Tries to extract a path
        /// </summary>
        /// <param name="graph"></param>
        /// <returns><see cref="true"/> if extracting is 
        /// successed and <see cref="false"/> if not</returns>
        public bool TryExtractPath()
        {
            if (ParentVertices.ContainsKey(End.Position))
            {
                Path = GetPath();
                Cost = Path.Sum(vertex => vertex.Cost.CurrentCost);
                Length = Path.Count();
                IsExtracted = Path.Contains(End);
            }
            return IsExtracted;
        }

        private GraphPath()
        {
            Path = new IVertex[] { };
        }

        private IEnumerable<IVertex> GetPath()
        {
            var temp = End;
            yield return temp;
            while (!IsStartOfPath(temp))
            {
                temp = ParentVertices[temp.Position];
                yield return temp;
            }
        }

        private bool IsStartOfPath(IVertex vertex)
        {
            return vertex.IsDefault 
                || ParentVertices[vertex.Position].IsEqual(Start);
        }
    }
}
