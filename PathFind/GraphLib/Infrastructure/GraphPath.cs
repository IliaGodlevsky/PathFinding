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

        public GraphPath(IGraph graph) : this()
        {
            TryExtractPath(graph);
        }

        public void HighlightPath()
        {
            Path.Where(vertex => vertex.IsRegularVertex())
                .ForEach(vertex => vertex.MarkAsPath());
        }

        /// <summary>
        /// Tries to extract a path from the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns><see cref="true"/> if extracting is 
        /// successed and <see cref="false"/> if not</returns>
        public bool TryExtractPath(IGraph graph)
        {
            if (graph.IsExtremeVerticesVisited())
            {
                Path = GetPath(graph.End);
                Cost = Path.Sum(vertex => vertex.Cost.CurrentCost);
                Length = Path.Count();
                IsExtracted = Path.FirstOrDefault(vertex => vertex.IsEnd == true) != null;
            }

            return IsExtracted;
        }

        private GraphPath()
        {
            Path = new IVertex[] { };
        }

        private IEnumerable<IVertex> GetPath(IVertex end)
        {
            var temp = end;

            while (!IsStartOfPath(temp))
            {
                yield return temp;
                temp = temp.ParentVertex;
            }
        }

        private bool IsStartOfPath(IVertex vertex)
        {
            return vertex.IsDefault || vertex.IsStart;
        }
    }
}
