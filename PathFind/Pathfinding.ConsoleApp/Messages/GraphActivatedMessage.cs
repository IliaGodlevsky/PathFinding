namespace Pathfinding.ConsoleApp.Messages
{
    internal sealed class GraphActivatedMessage
    {
        public int GraphId { get; }

        public GraphActivatedMessage(int graphId)
        {
            GraphId = graphId;
        }
    }
}
