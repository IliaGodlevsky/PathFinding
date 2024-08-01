namespace Pathfinding.Service.Interface.Visualization
{
    public interface IRangeVisualizable
    {
        bool IsVisualizedAsRange();

        void VisualizeAsSource();

        void VisualizeAsTarget();

        void VisualizeAsTransit();
    }
}
