namespace Pathfinding.App.Console.Messages.ViewModel
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
