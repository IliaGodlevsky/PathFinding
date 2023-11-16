using System;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class AlgorithmEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public int GraphId { get; set; }
    }
}
