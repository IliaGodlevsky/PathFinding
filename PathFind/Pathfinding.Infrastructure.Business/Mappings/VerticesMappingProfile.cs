using AutoMapper;
using Newtonsoft.Json;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class VerticesMappingProfile<T> : Profile
        where T : IVertex, new()
    {
        public VerticesMappingProfile()
        {
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
            CreateMap<VertexAssembleModel, T>();
            CreateMap<T, Vertex>()
                .ForMember(x => x.Coordinates, opt => opt.MapFrom(x => CoordinateToString(x.Position)))
                .ForMember(x => x.UpperValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.UpperValueOfRange))
                .ForMember(x => x.LowerValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.LowerValueOfRange))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => x.Cost.CurrentCost));
            CreateMap<Vertex, T>().ConstructUsing((x, context) => new T() { Position = FromStringToCoordinate(x.Coordinates) })
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<Vertex, VertexAssembleModel>()
                .ForMember(x => x.Position, opt => opt.MapFrom(x => x.Coordinates))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<T, VertexSerializationModel>();
            CreateMap<VertexSerializationModel, T>().ConstructUsing(x => new T() { Position = new Coordinate(x.Position.Coordinate) });
        }

        private string CoordinateToString(Coordinate coord)
        {
            var array = coord.CoordinatesValues.ToList();
            return JsonConvert.SerializeObject(array);
        }

        private Coordinate FromStringToCoordinate(string coordinate)
        {
            var deserialized = JsonConvert.DeserializeObject<List<int>>(coordinate);
            return new Coordinate(deserialized);
        }
    }
}
