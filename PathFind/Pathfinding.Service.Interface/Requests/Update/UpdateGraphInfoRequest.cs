namespace Pathfinding.Service.Interface.Requests.Update
{
    public class UpdateGraphInfoRequest
    {
        public int Id { get; }

        public int ObstaclesCount { get; }

        public UpdateGraphInfoRequest(int id, int obstaclesCount)
        {
            Id = id;
            ObstaclesCount = obstaclesCount;
        }
    }
}
