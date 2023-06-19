using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections;
using System.Dynamic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class CoordinatesJsonRepository : JsonRepository<CoordinatesModel>
    {
        private readonly ICoordinateFactory factory;

        protected override string Table { get; }

        public CoordinatesJsonRepository(DataStore storage, string tableName,
            ICoordinateFactory factory) 
            : base(storage)
        {
            this.factory = factory;
            Table = tableName;
        }

        protected override dynamic Map(CoordinatesModel item)
        {
            dynamic result = new ExpandoObject();
            result.Id = item.Id;
            result.AlgorithmId = item.AlgorithmId;
            result.Coordinates = item.Coordinates
                .Select(c => new { Coordinates = c.ToArray() })
                .ToArray();
            return result;
        }

        protected override CoordinatesModel Map(dynamic model)
        {
            var modelCoordinates = (IEnumerable)model.Coordinates;
            return new CoordinatesModel()
            {
                Id = model.Id,
                AlgorithmId = model.AlgorithmId,
                Coordinates = modelCoordinates.OfType<dynamic>()
                    .Select(c => factory.CreateCoordinate((int[])c.Coordinates))
                    .ToArray()
            };
        }
    }
}
