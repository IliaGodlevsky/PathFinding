namespace Pathfinding.Service.Interface.Visualization
{
    public interface IRangeVisualization<T>
        where T : IRangeVisualizable
    {
        bool IsVisualizedAsRange(T visualizable);

        void VisualizeAsSource(T visualizable);

        void VisualizeAsTarget(T visualizable);

        void VisualizeAsTransit(T visualizable);
    }
}
