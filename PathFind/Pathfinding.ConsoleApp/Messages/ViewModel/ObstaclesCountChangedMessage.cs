namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class ObstaclesCountChangedMessage
    {
        public int GraphId { get; }

        public int Delta { get; }

        public ObstaclesCountChangedMessage(int graphId, int delta)
        {
            GraphId = graphId;
            Delta = delta;
        }
    }
}
