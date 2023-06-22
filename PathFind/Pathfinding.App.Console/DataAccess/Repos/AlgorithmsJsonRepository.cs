using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Entities.JsonEntities;
using Pathfinding.App.Console.DataAccess.Models;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class AlgorithmsJsonRepository : JsonRepository<AlgorithmModel, JsonAlgorithmEntity>
    {
        public AlgorithmsJsonRepository(DataStore connection) 
            : base(connection)
        {

        }

        protected override string Table { get; } = "algorithms";

        protected override JsonAlgorithmEntity Map(AlgorithmModel item)
        {
            return new()
            {
                Id = item.Id,
                GraphId = item.GraphId,
                Name = item.Name,
            };
        }

        protected override AlgorithmModel Map(JsonAlgorithmEntity model)
        {
            return new()
            {
                Id = model.Id,
                GraphId = model.GraphId,
                Name = model.Name,
            };
        }
    }
}
