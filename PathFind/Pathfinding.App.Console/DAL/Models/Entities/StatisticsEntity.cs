using System;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    internal class StatisticsEntity
    {
        public int Id { get; set; }

        public int AlgorithmId { get; set; }

        public TimeSpan? Speed { get; set; }
    }
}
