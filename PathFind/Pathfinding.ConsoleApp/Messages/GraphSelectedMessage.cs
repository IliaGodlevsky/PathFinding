namespace Pathfinding.ConsoleApp.Messages
{
    internal sealed class GraphSelectedMessage
    {
        public int GraphId { get; }

        public GraphSelectedMessage(int graphId)
        {
            GraphId = graphId;
        }
    }
}
