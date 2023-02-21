namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IPathVisualization<T>
        where T : IPathVisualizable
    {
        bool IsVisualizedAsPath(T visualizable);

        void VisualizeAsPath(T visualizable);
    }
}
