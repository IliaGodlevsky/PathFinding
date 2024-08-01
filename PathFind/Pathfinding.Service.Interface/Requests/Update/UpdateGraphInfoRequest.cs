namespace Pathfinding.Service.Interface.Requests.Update
{
    public class UpdateGraphInfoRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ObstaclesCount { get; set; }
    }
}
