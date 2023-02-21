namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IGraphVisualization<T>
        where T : IGraphVisualizable
    {
        void VisualizeAsRegular(T visualizable);

        void VisualizeAsObstacle(T visualizable);
    }
}
