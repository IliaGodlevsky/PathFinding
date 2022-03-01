namespace GraphLib.Interfaces
{
    public interface IVisualizable
    {
        void VisualizeAsTarget();

        void VisualizeAsRegular();

        void VisualizeAsObstacle();

        void VisualizeAsPath();

        void VisualizeAsSource();

        void VisualizeAsVisited();

        void VisualizeAsEnqueued();

        void VisualizeAsIntermediate();

        void VisualizeAsMarkedToReplaceIntermediate();

        bool IsVisualizedAsPath { get; }

        bool IsVisualizedAsEndPoint { get; }
    }
}
