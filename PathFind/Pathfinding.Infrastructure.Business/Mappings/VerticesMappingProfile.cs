using AutoMapper;
using Newtonsoft.Json;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class VerticesMappingProfile<T> : Profile
        where T : IVertex
    {
        private readonly IVertexFactory<T> vertexFactory;

        public VerticesMappingProfile(IVertexFactory<T> vertexFactory)
        {
            this.vertexFactory = vertexFactory;
            CreateMap<Coordinate, string>()
                .ConvertUsing(x => CoordinateToString(x));
            CreateMap<string, Coordinate>()
                .ConvertUsing(x => FromStringToCoordinate(x));
            CreateMap<IVertexCost, VertexCostModel>()
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => x.CurrentCost))
                .ForMember(x => x.UpperValueOfRange, opt => opt.MapFrom(x => x.CostRange.UpperValueOfRange))
                .ForMember(x => x.LowerValueOfRange, opt => opt.MapFrom(x => x.CostRange.LowerValueOfRange));
            CreateMap<VertexCostModel, IVertexCost>()
                .ConvertUsing(x => new VertexCost(x.Cost, new(x.UpperValueOfRange, x.LowerValueOfRange)));
            CreateMap<VertexAssembleModel, T>()
                .ConstructUsing(x => vertexFactory.CreateVertex(x.Coordinate));
            CreateMap<T, Vertex>()
                .ForMember(x => x.Coordinates, opt => opt.MapFrom(x => CoordinateToString(x.Position)))
                .ForMember(x => x.UpperValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.UpperValueOfRange))
                .ForMember(x => x.LowerValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.LowerValueOfRange))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => x.Cost.CurrentCost));
            CreateMap<Vertex, T>().ConstructUsing((x, context) => vertexFactory.CreateVertex(FromStringToCoordinate(x)))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<Vertex, VertexAssembleModel>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => x.Coordinates))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<T, VertexSerializationModel>()
                .ForMember(x => x.Neighbors, opt => opt.MapFrom(x => x.Neighbours.GetCoordinates().ToReadOnly()));
            CreateMap<VertexSerializationModel, T>().ConstructUsing(x => vertexFactory.CreateVertex(new Coordinate(x.Position.Coordinate.ToArray())));
            CreateMap<IEnumerable<VertexSerializationModel>, IEnumerable<T>>().ConstructUsing(ToVertices);
        }

        private string CoordinateToString(Coordinate coord)
        {
            var array = coord.CoordinatesValues.ToList();
            var serialized = JsonConvert.SerializeObject(array);
            return serialized;
        }

        private Coordinate FromStringToCoordinate(string coordinate)
        {
            var deserialized = JsonConvert.DeserializeObject<List<int>>(coordinate);
            return new Coordinate(deserialized);
        }

        private Coordinate FromStringToCoordinate(Vertex entity)
        {
            var coordinates = JsonConvert.DeserializeObject<int[]>(entity.Coordinates);
            return new Coordinate(coordinates);
        }

        private IEnumerable<T> ToVertices(IEnumerable<VertexSerializationModel> dtos,
            ResolutionContext context)
        {
            var vertices = dtos.Select(context.Mapper.Map<T>).ToDictionary(x => x.Position);
            foreach (var dto in dtos)
            {
                var position = context.Mapper.Map<Coordinate>(dto.Position);
                var vertex = vertices[position];
                var neighbors = context.Mapper
                    .Map<Coordinate[]>(dto.Neighbors)
                    .Select(x => vertices[x])
                    .ToReadOnly();
                vertex.Neighbours.AddRange(neighbors.OfType<IVertex>());
            }
            return vertices.Values;
        }
    }
}
