namespace Pathfinding.Domain.Core
{
    public class AlgorithmRun : IEntity<int>
    {
        public int Id { get; set; }

        public int GraphId { get; set; }

        public string AlgorithmId { get; set; }
    }
}
