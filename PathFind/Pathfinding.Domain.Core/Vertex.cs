namespace Pathfinding.Domain.Core
{
    public class Vertex : IEntity<int>
    {
        public int Id { get; set; }

        public int GraphId { get; set; }

        public int Order { get; set; }

        public byte[] Coordinates { get; set; }

        public int Cost { get; set; }

        public int UpperValueRange { get; set; }

        public int LowerValueRange { get; set; }

        public bool IsObstacle { get; set; }
    }
}
