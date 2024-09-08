namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class GraphSelectedMessage
    {
        public int[] GraphIds { get; }

        public GraphSelectedMessage(int[] graphId)
        {
            GraphIds = graphId;
        }
    }
}
