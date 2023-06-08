using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.Entities
{
    internal class AlgorithmEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid GraphId { get; set; }
    }
}
