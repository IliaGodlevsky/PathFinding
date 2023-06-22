using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Entities.JsonEntities;
using Pathfinding.App.Console.DataAccess.Models;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class CostsJsonRepository : JsonRepository<CostsModel, JsonCostsEntity>
    {
        public CostsJsonRepository(DataStore storage)
            : base(storage)
        {

        }

        protected override string Table { get; } = "costs";

        protected override JsonCostsEntity Map(CostsModel item)
        {
            return new()
            {
                Id = item.Id,
                AlgorithmId = item.AlgorithmId,
                Costs = item.Costs.ToArray()
            };
        }

        protected override CostsModel Map(JsonCostsEntity model)
        {
            return new()
            {
                Id = model.Id,
                AlgorithmId = model.AlgorithmId,
                Costs = model.Costs
            };
        }
    }
}
