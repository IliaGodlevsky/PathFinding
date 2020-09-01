using GraphLibrary.Algorithm;
using GraphLibrary.Common.Constants;
using GraphLibrary.Extensions;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.Common.Extensions
{
    public static class IPathFindAlgorithmExtension
    {
        public static void DrawPath(this IPathFindAlgorithm algo, Func<IVertex, IVertex> parentVertexFunction)
        {
            var vertex = algo.Graph.End;
            while (!vertex.IsStart)
            {
                algo.StatCollector.IncludeVertexInStatistics(vertex);
                vertex = parentVertexFunction(vertex);
                if (vertex.IsSimpleVertex())
                    vertex.MarkAsPath();
                algo.Pauser.Pause(AlgorithmExecutionDelay.PATH_DRAW_PAUSE);
            }
        }

        public static bool IsRightGraphSettings(this IPathFindAlgorithm algo)
        {
            return algo.Graph != null
                && algo.Graph?.End != null
                && algo.Graph?.Start != null;
        }

        public static void VisitVertex(this IPathFindAlgorithm algorithm, IVertex vertex)
        {
            vertex.IsVisited = true;
            if (vertex.IsSimpleVertex())
            {
                vertex.MarkAsCurrentlyLooked();
                algorithm.Pauser.Pause(AlgorithmExecutionDelay.VISIT_PAUSE);
                vertex.MarkAsVisited();
            }
            algorithm.StatCollector.Visited();
        }

        public static bool HasFoundPath(this IPathFindAlgorithm algorithm)
        {
            return algorithm.Graph?.End.IsVisited == true;
        }
    }
}
