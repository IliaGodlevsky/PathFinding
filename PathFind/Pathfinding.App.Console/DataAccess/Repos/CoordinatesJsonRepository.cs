using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Entities.JsonEntities;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.GraphLib.Factory.Interface;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class CoordinatesJsonRepository : JsonRepository<CoordinatesModel, JsonCoordinatesEntity>
    {
        private readonly ICoordinateFactory factory;

        protected override string Table { get; }

        public CoordinatesJsonRepository(DataStore storage,ICoordinateFactory factory) 
            : base(storage)
        {
            this.factory = factory;
            Table = "coordinates";
        }

        protected override JsonCoordinatesEntity Map(CoordinatesModel item)
        {
            var result = new JsonCoordinatesEntity();
            result.Id = item.Id;
            result.AlgorithmId = item.AlgorithmId;
            result.Coordinates = item.Coordinates
                .Select(c => new GraphJsonCoordinates { Coordinates = c.ToArray() })
                .ToArray();
            return result;
        }

        protected override CoordinatesModel Map(JsonCoordinatesEntity model)
        {
            var modelCoordinates = model.Coordinates;
            return new CoordinatesModel()
            {
                Id = model.Id,
                AlgorithmId = model.AlgorithmId,
                Coordinates = modelCoordinates
                    .Select(c => factory.CreateCoordinate(c.Coordinates))
                    .ToArray()
            };
        }
    }
}
