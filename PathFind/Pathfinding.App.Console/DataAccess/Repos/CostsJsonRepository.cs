using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class CostsJsonRepository : JsonRepository<CostsModel>
    {
        public CostsJsonRepository(DataStore storage)
            : base(storage)
        {

        }

        protected override string Table { get; } = "costs";

        protected override dynamic Map(CostsModel item)
        {
            return item;
        }

        protected override CostsModel Map(dynamic model)
        {
            return (CostsModel)model;
        }
    }
}
