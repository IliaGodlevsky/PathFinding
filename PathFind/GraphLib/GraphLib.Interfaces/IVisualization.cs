namespace GraphLib.Interfaces
{
    public interface IVisualization<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        bool IsVisualizedAsPath(TVertex vertex);

        bool IsVisualizedAsEndPoint(TVertex vertex);

        void VisualizeAsTarget(TVertex vertex);

        void VisualizeAsObstacle(TVertex vertex);

        void VisualizeAsSource(TVertex vertex);

        void VisualizeAsIntermediate(TVertex vertex);

        void VisualizeAsRegular(TVertex vertex);

        void VisualizeAsPath(TVertex vertex);

        void VisualizeAsVisited(TVertex vertex);

        void VisualizeAsEnqueued(TVertex vertex);

        void VisualizeAsMarkedToReplaceIntermediate(TVertex vertex);
    }
}