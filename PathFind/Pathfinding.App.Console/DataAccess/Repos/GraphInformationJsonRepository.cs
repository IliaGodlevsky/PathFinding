using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;
using System;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    public sealed class JsonInformation : IIdentityItem<long>
    {
        public JsonInformation() { }
        public long Id { get; set; }

        public long GraphId { get; set; }

        public string Description { get; set; }
    }

    internal sealed class GraphInformationJsonRepository : JsonRepository<GraphInformationModel, JsonInformation>
    {
        public GraphInformationJsonRepository(DataStore storage) 
            : base(storage)
        {
        }

        protected override string Table { get; } = "graphinfo";

        protected override JsonInformation Map(GraphInformationModel item)
        {
            return new()
            {
                Id = item.Id,
                GraphId = item.GraphId,
                Description = item.Description
            };
        }

        protected override GraphInformationModel Map(JsonInformation model)
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
