namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface ITotalVisualization<T> : IPathfindingVisualization<T>, IGraphVisualization<T>,
        IRangeVisualization<T>, IPathVisualization<T>
        where T : IPathfindingVisualizable, IGraphVisualizable, IRangeVisualizable, IPathVisualizable
    {

    }
}