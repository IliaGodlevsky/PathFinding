using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class AlgorithmsJsonRepository : JsonRepository<AlgorithmModel>
    {
        public AlgorithmsJsonRepository(DataStore connection) 
            : base(connection)
        {

        }

        protected override string Table { get; } = "algorithms";

        protected override dynamic Map(AlgorithmModel item)
        {
            return item;
        }

        protected override AlgorithmModel Map(dynamic model)
        {
            return (AlgorithmModel)model;
        }
    }
}
