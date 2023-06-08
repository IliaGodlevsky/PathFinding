using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.Entities
{
    internal class VerticesEntity :IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid GraphId { get; set; }

        public string Coordinates { get; set; }
    }
}
