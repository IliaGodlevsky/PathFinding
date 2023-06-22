using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;
using System;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    public sealed class JsonAlgorithm : IIdentityItem<long>
    {
        public JsonAlgorithm() { }
        public long Id { get; set; }

        public long GraphId { get; set; }

        public string Name { get; set; }
    }

    internal sealed class AlgorithmsJsonRepository : JsonRepository<AlgorithmModel, JsonAlgorithm>
    {
        public AlgorithmsJsonRepository(DataStore connection) 
            : base(connection)
        {

        }

        protected override string Table { get; } = "algorithms";

        protected override JsonAlgorithm Map(AlgorithmModel item)
        {
            return new()
            {
                Id = item.Id,
                GraphId = item.GraphId,
                Name = item.Name,
            };
        }

        protected override AlgorithmModel Map(JsonAlgorithm model)
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
