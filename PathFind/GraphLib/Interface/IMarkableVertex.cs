namespace GraphLib.Interface
{
    public interface IMarkableVertex
    {
        void MarkAsEnd();

        void MarkAsSimpleVertex();

        void MarkAsObstacle();

        void MarkAsPath();

        void MarkAsStart();

        void MarkAsVisited();

        void MarkAsEnqueued();
    }
}
