using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Infrastructure
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
        public int PathLength { get; private set; }

        /// <summary>
        /// Returns the sum of costs of the vertices in the path
        /// </summary>
        public int PathCost { get; private set; }

        public GraphPath(IGraph graph) : this()
        {
            TryExtractPath(graph);
        }

        public void HighlightPath()
        {
            foreach (IVertex vertex in Path)
            {
                if (vertex.IsSimpleVertex())
                {
                    vertex.MarkAsPath();
                }
            }
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
                PathCost = Path.Sum(vertex => (int)vertex.Cost);
                PathLength = Path.Count();
                IsExtracted = Path.Any();
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
