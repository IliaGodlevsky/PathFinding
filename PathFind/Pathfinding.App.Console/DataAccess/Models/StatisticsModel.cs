using System;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class StatisticsModel : IIdentityItem<Guid>
    {
        public Guid Id { get; set; }

        public Guid AlgorithmId { get; set; }

        public string AlgorithmName { get; set; }

        public TimeSpan Elapsed { get; set; }

        public int Visited { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }
    }
}
