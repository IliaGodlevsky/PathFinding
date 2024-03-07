namespace Pathfinding.App.Console.DAL.Models.Entities
{
    internal class GraphStateEntity
    {
        public int Id { get; set; }

        public int AlgorithmRunId { get; set; }

        public byte[] Costs { get; set; }

        public byte[] Obstacles { get; set; }

        public byte[] Range { get; set; }
    }
}
