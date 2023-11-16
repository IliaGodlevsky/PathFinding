using Pathfinding.App.Console.DataAccess.Entities;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class StatisticsInMemoryStorage : InMemoryStorage<StatisticEntity, int>
    {
        private static int id = 0;

        protected override int NextId => id++;
    }
}
