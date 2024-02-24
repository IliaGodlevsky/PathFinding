namespace Pathfinding.App.Console.DAL.Models.Entities
{
    internal class SubAlgorithmEntity
    {
        public int Id { get; set; }

        public int AlgorithmRunId { get; set; }

        public int Order { get; set; }

        public byte[] Visited { get; set; }

        public byte[] Path { get; set; }
    }
}
