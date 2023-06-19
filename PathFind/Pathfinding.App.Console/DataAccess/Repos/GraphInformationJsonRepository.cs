using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class GraphInformationJsonRepository : JsonRepository<GraphInformationModel>
    {
        public GraphInformationJsonRepository(DataStore storage) 
            : base(storage)
        {
        }

        protected override string Table { get; } = "graphinfo";

        protected override dynamic Map(GraphInformationModel item)
        {
            return item;
        }

        protected override GraphInformationModel Map(dynamic model)
        {
            return (GraphInformationModel)model;
        }
    }
}
