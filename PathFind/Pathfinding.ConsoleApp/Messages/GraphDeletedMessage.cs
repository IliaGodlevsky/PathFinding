namespace Pathfinding.ConsoleApp.Messages
{
    internal sealed class GraphDeletedMessage
    {
        public int GraphId { get; }

        public GraphDeletedMessage(int graphId)
        {
            GraphId = graphId;
        }
    }
}
