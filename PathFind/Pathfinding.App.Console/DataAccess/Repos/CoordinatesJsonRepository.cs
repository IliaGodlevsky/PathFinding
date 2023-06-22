using JsonFlatFileDataStore;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.GraphLib.Factory.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    public sealed class JsonCoordinates : IIdentityItem<long>
    {
        public JsonCoordinates() { }
        public long Id { get; set; }

        public long AlgorithmId { get; set; }

        public GraphJsonCoordinates[] Coordinates { get; set; }
    }

    internal sealed class CoordinatesJsonRepository : JsonRepository<CoordinatesModel, JsonCoordinates>
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

        protected override JsonCoordinates Map(CoordinatesModel item)
        {
            var result = new JsonCoordinates();
            result.Id = item.Id;
            result.AlgorithmId = item.AlgorithmId;
            result.Coordinates = item.Coordinates
                .Select(c => new GraphJsonCoordinates { Coordinates = c.ToArray() })
                .ToArray();
            return result;
        }

        protected override CoordinatesModel Map(JsonCoordinates model)
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
