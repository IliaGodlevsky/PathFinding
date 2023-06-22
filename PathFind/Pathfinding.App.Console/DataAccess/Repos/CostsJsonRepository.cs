using JsonFlatFileDataStore;
using Org.BouncyCastle.Asn1.X509;
using Pathfinding.App.Console.DataAccess.Models;
using System;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    public sealed class JsonCosts : IIdentityItem<long>
    {
        public JsonCosts() { }
        public long Id { get; set; }

        public long AlgorithmId { get; set; }

        public int[] Costs { get; set; }
    }

    internal sealed class CostsJsonRepository : JsonRepository<CostsModel, JsonCosts>
    {
        public CostsJsonRepository(DataStore storage)
            : base(storage)
        {

        }

        protected override string Table { get; } = "costs";

        protected override JsonCosts Map(CostsModel item)
        {
            return new()
            {
                Id = item.Id,
                AlgorithmId = item.AlgorithmId,
                Costs = item.Costs.ToArray()
            };
        }

        protected override CostsModel Map(JsonCosts model)
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
