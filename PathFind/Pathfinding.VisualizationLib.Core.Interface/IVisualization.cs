namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IVisualization<T>
        where T : IVisualizable
    {
        bool IsVisualizedAsPath(T visualizable);

        bool IsVisualizedAsPathfindingRange(T visualizable);

        void VisualizeAsTarget(T visualizable);

        void VisualizeAsObstacle(T visualizable);

        void VisualizeAsSource(T visualizable);

        void VisualizeAsIntermediate(T visualizable);

        void VisualizeAsRegular(T visualizable);

        void VisualizeAsPath(T visualizable);

        void VisualizeAsVisited(T visualizable);

        void VisualizeAsEnqueued(T visualizable);

        void VisualizeAsMarkedToReplaceIntermediate(T visualizable);
    }
}