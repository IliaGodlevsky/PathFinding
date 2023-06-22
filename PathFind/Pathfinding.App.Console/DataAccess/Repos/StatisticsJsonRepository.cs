using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.Entities.JsonEntities;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class StatisticsJsonRepository : JsonRepository<StatisticsModel, JsonStatisticsEntity>
    {
        public StatisticsJsonRepository(DataStore storage) 
            : base(storage)
        {
        }

        protected override string Table { get; } = "statistics";

        protected override JsonStatisticsEntity Map(StatisticsModel item)
        {
            return new JsonStatisticsEntity()
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

        protected override StatisticsModel Map(JsonStatisticsEntity model)
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
