namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IVisualization
    {
        bool IsVisualizedAsPath(IVisualizable vertex);

        bool IsVisualizedAsEndPoint(IVisualizable vertex);

        void VisualizeAsTarget(IVisualizable vertex);

        void VisualizeAsObstacle(IVisualizable vertex);

        void VisualizeAsSource(IVisualizable vertex);

        void VisualizeAsIntermediate(IVisualizable vertex);

        void VisualizeAsRegular(IVisualizable vertex);

        void VisualizeAsPath(IVisualizable vertex);

        void VisualizeAsVisited(IVisualizable vertex);

        void VisualizeAsEnqueued(IVisualizable vertex);

        void VisualizeAsMarkedToReplaceIntermediate(IVisualizable vertex);
    }
}