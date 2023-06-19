using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class StatisticsJsonRepository : JsonRepository<StatisticsModel>
    {
        public StatisticsJsonRepository(DataStore storage) 
            : base(storage)
        {
        }

        protected override string Table { get; } = "statistics";

        protected override dynamic Map(StatisticsModel item)
        {
            return item;
        }

        protected override StatisticsModel Map(dynamic model)
        {
            return (StatisticsModel)model;
        }
    }
}
