namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IPathfindingVisualization<T>
        where T : IPathfindingVisualizable
    {
        void VisualizeAsVisited(T visualizable);

        void VisualizeAsEnqueued(T visualizable);
    }
}
