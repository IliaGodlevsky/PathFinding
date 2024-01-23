using Pathfinding.App.Console.DAL.Interface;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    internal class SubAlgorithmEntity : IEntity
    {
        public int Id { get; set; }

        public int AlgorithmId { get; set; }
    }
}
