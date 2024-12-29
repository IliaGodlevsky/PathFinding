namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal class RunsDeletedMessage
    {
        public int[] RunIds { get; }

        public RunsDeletedMessage(int[] runIds)
        {
            RunIds = runIds;
        }
    }
}
