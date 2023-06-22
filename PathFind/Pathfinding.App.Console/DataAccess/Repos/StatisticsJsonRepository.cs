using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;
using System.Globalization;
using System;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    public sealed class JsonStatistics : IIdentityItem<long>
    {
        public JsonStatistics() { }
        public long Id { get; set; }

        public long AlgorithmId { get; set; }

        public string AlgorithmName { get; set; }

        public TimeSpan Elapsed { get; set; }

        public int Visited { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }
    }

    internal sealed class StatisticsJsonRepository : JsonRepository<StatisticsModel, JsonStatistics>
    {
        public StatisticsJsonRepository(DataStore storage) 
            : base(storage)
        {
        }

        protected override string Table { get; } = "statistics";

        protected override JsonStatistics Map(StatisticsModel item)
        {
            return new JsonStatistics()
            {
                Id = item.Id,
                AlgorithmId = item.AlgorithmId,
                AlgorithmName = item.AlgorithmName,
                Elapsed = item.Elapsed,
                Visited = item.Visited,
                Steps = item.Steps,
                Cost = item.Cost
            };
        }

        protected override StatisticsModel Map(JsonStatistics model)
        {
            return new()
            {
                Id = model.Id,
                AlgorithmId = model.AlgorithmId,
                AlgorithmName = model.AlgorithmName,
                Elapsed = model.Elapsed,
                Visited = model.Visited,
                Steps = model.Steps,
                Cost = model.Cost
            };
        }
    }
}
