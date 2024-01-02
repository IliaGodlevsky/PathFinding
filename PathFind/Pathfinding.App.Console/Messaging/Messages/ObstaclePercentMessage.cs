namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class ObstaclePercentMessage
    {
        public int ObstaclePercent { get; }

        public ObstaclePercentMessage(int obstaclePercent)
        {
            ObstaclePercent = obstaclePercent;
        }
    }
}
