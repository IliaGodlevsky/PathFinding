namespace GraphLib.Interfaces
{
    /// <summary>
    /// An interface, that provides methods
    /// for marking vertices as they are
    /// processed by pathfinding algorithm
    /// </summary>
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

        bool IsVisualizedAsPath { get; }

        bool IsVisualizedAsEndPoint { get; }
    }
}
