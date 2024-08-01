namespace Pathfinding.Domain.Core
{
    public class Neighbor : IEntity<int>
    {
        public int Id { get; set; }

        public int NeighborId { get; set; }

        public int VertexId { get; set; }
    }
}
