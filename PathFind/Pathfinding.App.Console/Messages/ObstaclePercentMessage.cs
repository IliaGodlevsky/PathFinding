namespace Pathfinding.App.Console.Messages
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
