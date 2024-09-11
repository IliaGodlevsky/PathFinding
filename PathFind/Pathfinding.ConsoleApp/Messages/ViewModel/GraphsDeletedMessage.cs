namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class GraphsDeletedMessage
    {
        public int[] GraphIds { get; }

        public GraphsDeletedMessage(int[] graphIds)
        {
            GraphIds = graphIds;
        }
    }
}
