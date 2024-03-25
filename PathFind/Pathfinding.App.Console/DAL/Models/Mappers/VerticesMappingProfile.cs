using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class VerticesMappingProfile<T> : Profile
        where T : IVertex
    {
        public VerticesMappingProfile(IVertexFactory<T> vertexFactory)
        {
            CreateMap<IVertexCost, VertexCostDto>()
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => x.CurrentCost))
                .ForMember(x => x.UpperValueOfRange, opt => opt.MapFrom(x => x.CostRange.UpperValueOfRange))
                .ForMember(x => x.LowerValueOfRange, opt => opt.MapFrom(x => x.CostRange.LowerValueOfRange));
            CreateMap<VertexCostDto, IVertexCost>()
                .ConvertUsing(x => new VertexCost(x.Cost, new(x.UpperValueOfRange, x.LowerValueOfRange)));
            CreateMap<VertexAssembleDto, T>()
                .ConstructUsing(x => vertexFactory.CreateVertex(x.Coordinate));
            CreateMap<T, VertexEntity>()
                .ForMember(x => x.X, opt => opt.MapFrom(x => x.Position.GetX()))
                .ForMember(x => x.Y, opt => opt.MapFrom(x => x.Position.GetY()))
                .ForMember(x => x.UpperValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.UpperValueOfRange))
                .ForMember(x => x.LowerValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.LowerValueOfRange))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => x.Cost.CurrentCost));
            CreateMap<VertexEntity, T>().ConstructUsing(x => vertexFactory.CreateVertex(new Coordinate(x.X, x.Y)))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<VertexEntity, VertexAssembleDto>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => new Coordinate(x.X, x.Y)))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<T, VertexSerializationDto>()
                .ForMember(x => x.Neighbors, opt => opt.MapFrom(x => x.Neighbours.GetCoordinates().ToReadOnly()));
            CreateMap<VertexSerializationDto, T>().ConstructUsing(x => vertexFactory.CreateVertex(new Coordinate(x.Position.Coordinate.ToArray())));
            CreateMap<IEnumerable<VertexSerializationDto>, IEnumerable<T>>().ConstructUsing(ToVertices);
        }

        private IEnumerable<T> ToVertices(IEnumerable<VertexSerializationDto> dtos,
            ResolutionContext context)
        {
            var vertices = dtos.Select(context.Mapper.Map<T>).ToDictionary(x => x.Position);
            foreach (var dto in dtos)
            {
                var position = context.Mapper.Map<ICoordinate>(dto.Position);
                var vertex = vertices[position];
                var neighbors = context.Mapper
                    .Map<ICoordinate[]>(dto.Neighbors)
                    .Select(x => vertices[x])
                    .ToReadOnly();
                vertex.Neighbours.AddRange(neighbors.OfType<IVertex>());
            }
            return vertices.Values;
        }
    }
}
