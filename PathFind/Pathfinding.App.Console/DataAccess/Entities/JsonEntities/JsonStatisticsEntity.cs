using System;

namespace Pathfinding.App.Console.DataAccess.Entities.JsonEntities
{
    public class JsonStatisticsEntity : IIdentityItem<long>
    {
        public long Id { get; set; }

        public long AlgorithmId { get; set; }

        public string AlgorithmName { get; set; }

        public TimeSpan Elapsed { get; set; }

        public int Visited { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }
    }
}