namespace Pathfinding.ConsoleApp.Messages.ViewModel
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
