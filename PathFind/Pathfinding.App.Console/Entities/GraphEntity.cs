using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.Entities
{
    internal class GraphEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public byte[] Vertices { get; set; }
    }
}
