namespace Pathfinding.Service.Interface.Visualization
{
    public interface IPathVisualization<T>
        where T : IPathVisualizable
    {
        bool IsVisualizedAsPath(T visualizable);

        void VisualizeAsPath(T visualizable);
    }
}
