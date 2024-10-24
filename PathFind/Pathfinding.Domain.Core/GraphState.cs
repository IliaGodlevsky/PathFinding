namespace Pathfinding.Domain.Core
{
    public class GraphState : IEntity<int>
    {
        public int Id { get; set; }

        public int AlgorithmRunId { get; set; }

        public byte[] Costs { get; set; }

        public byte[] Obstacles { get; set; }

        public byte[] Regulars { get; set; }

        public byte[] Range { get; set; }
    }
}
