namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class GraphDeletedMessage
    {
        public int[] GraphIds { get; }

        public GraphDeletedMessage(int[] graphIds)
        {
            GraphIds = graphIds;
        }
    }
}
