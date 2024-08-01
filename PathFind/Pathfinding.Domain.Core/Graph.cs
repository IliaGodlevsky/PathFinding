namespace Pathfinding.Domain.Core
{
    public class Graph : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Dimensions { get; set; }

        public int ObstaclesCount { get; set; }
    }
}
