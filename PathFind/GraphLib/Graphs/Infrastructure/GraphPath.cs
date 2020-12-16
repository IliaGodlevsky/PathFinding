using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Infrastructure
{
    public sealed class GraphPath : IEnumerable<IVertex>
    {
        public GraphPath(IGraph graph) : this()
        {
            ExtractPath(graph);
        }

        public bool IsExtracted { get; private set; }

        public int PathLength { get; private set; }

        public int PathCost { get; private set; }

        public void HighlightPath()
        {
            foreach (IVertex vertex in path)
            {
                if (vertex.IsSimpleVertex())
                {
                    vertex.MarkAsPath();
                }
            }
        }

        public void ExtractPath(IGraph graph)
        {
            if (graph.IsExtremeVerticesVisited())
            {
                path = GetPath(graph.End);
                PathCost = path.Sum(vertex => (int)vertex.Cost);
                PathLength = path.Count();
                IsExtracted = path.Any();
            }
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            return path.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return path.GetEnumerator();
        }

        private GraphPath()
        {
            path = new IVertex[] { };
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

        private IEnumerable<IVertex> path;
    }
}
