namespace GraphLib.Interfaces
{
    public interface IMarkable
    {
        void MarkAsEnd();

        void MarkAsRegular();

        void MarkAsObstacle();

        void MarkAsPath();

        void MarkAsStart();

        void MarkAsVisited();

        void MarkAsEnqueued();
    }
}
