namespace Pathfinding.Domain.Core
{
    public class SubAlgorithm : IEntity<int>
    {
        public int Id { get; set; }

        public int AlgorithmRunId { get; set; }

        public int Order { get; set; }

        public byte[] Visited { get; set; }

        public byte[] Path { get; set; }
    }
}
