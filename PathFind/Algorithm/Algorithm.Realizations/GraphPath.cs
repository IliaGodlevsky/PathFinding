using Algorithm.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Algorithm.Сompanions;
using Common.Extensions;


namespace Algorithm.Realizations
{
    public sealed class GraphPath : IGraphPath
    {
        private IVertex[] Empty => new IVertex[] { };

        public IEnumerable<IVertex> Path
        {
            get
            {
                if (path == null)
                {
                    path = UnwindPath().ToArray();
                    if (!IsPathFormed())
                    {
                        path = Empty;
                    }
                    path = path.Except(endPoints.Start).ToArray();
                }

                return path;
            }
        }

        public GraphPath(ParentVertices parentVertices, 
            IEndPoints endPoints, IGraph graph) : this()
        {
            this.parentVertices = parentVertices;
            this.graph = graph;
            this.endPoints = endPoints;
        }

        private GraphPath()
        {
           
        }

        private IEnumerable<IVertex> UnwindPath()
        {
            var vertex = endPoints.End;
            yield return vertex;
            var parent = parentVertices.GetParent(vertex);
            while (graph.AreNeighbours(vertex, parent))
            {
                yield return parent;
                vertex = parent;
                parent = parentVertices.GetParent(vertex);
            }
        }

        private bool IsPathFormed()
        {
            return path.Length >= RequiredNumberOfVerticesForCompletePath;
        }

        private const int RequiredNumberOfVerticesForCompletePath = 2;

        private readonly ParentVertices parentVertices;
        private readonly IGraph graph;
        private readonly IEndPoints endPoints;
        private IVertex[] path;
    }
}
