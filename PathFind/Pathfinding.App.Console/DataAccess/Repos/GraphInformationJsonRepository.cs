using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Entities.JsonEntities;
using Pathfinding.App.Console.DataAccess.Models;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class GraphInformationJsonRepository : JsonRepository<GraphInformationModel, JsonInformationEntity>
    {
        public GraphInformationJsonRepository(DataStore storage) 
            : base(storage)
        {
        }

        protected override string Table { get; } = "graphinfo";

        protected override JsonInformationEntity Map(GraphInformationModel item)
        {
            return new()
            {
                Id = item.Id,
                GraphId = item.GraphId,
                Description = item.Description
            };
        }

        protected override GraphInformationModel Map(JsonInformationEntity model)
        {
            return new GraphInformationModel()
            {
                Id = model.Id,
                GraphId = model.GraphId,
                Description = model.Description
            };
        }
    }
}
