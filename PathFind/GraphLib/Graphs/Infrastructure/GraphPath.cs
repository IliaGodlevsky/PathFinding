using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Infrastructure
{
    public sealed class GraphPath
    {
        public IEnumerable<IVertex> Path { get; private set; }

        public bool IsExtracted { get; private set; }

        public int PathLength { get; private set; }

        public int PathCost { get; private set; }

        public GraphPath(IGraph graph) : this()
        {
            ExtractPath(graph);
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

        public void ExtractPath(IGraph graph)
        {
            if (graph.IsExtremeVerticesVisited())
            {
                Path = GetPath(graph.End);
                PathCost = Path.Sum(vertex => (int)vertex.Cost);
                PathLength = Path.Count();
                IsExtracted = Path.Any();
            }
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
