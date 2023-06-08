using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.Entities
{
    internal class StatisticsEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public TimeSpan Time { get; set; }

        public double Cost { get; set; }

        public int Steps { get; set; }

        public int VisitedVertices { get; set; }

        public Guid AlgorithmId { get; set; }
    }
}
