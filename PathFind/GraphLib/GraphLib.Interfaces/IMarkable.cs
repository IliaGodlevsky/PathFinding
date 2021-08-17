namespace GraphLib.Interfaces
{
    /// <summary>
    /// An interface, that provides methods
    /// for marking vertices as they are
    /// processed by pathfinding algorithm
    /// </summary>
    public interface IMarkable
    {
        void MarkAsTarget();

        void MarkAsRegular();

        void MarkAsObstacle();

        void MarkAsPath();

        void MarkAsSource();

        void MarkAsVisited();

        void MarkAsEnqueued();

        void MarkAsIntermediate();
    }
}
