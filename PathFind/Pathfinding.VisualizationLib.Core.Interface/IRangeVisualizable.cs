namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IRangeVisualizable
    {
        bool IsVisualizedAsRange();

        void VisualizeAsSource();

        void VisualizeAsTarget();

        void VisualizeAsTransit();
    }
}
