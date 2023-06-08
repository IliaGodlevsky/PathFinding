using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.Dto
{
    internal class AlgorithmDto : IDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid GraphId { get; set; }
    }
}
