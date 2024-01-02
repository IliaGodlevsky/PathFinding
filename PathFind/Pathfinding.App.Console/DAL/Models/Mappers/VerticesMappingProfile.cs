﻿using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class VerticesMappingProfile : Profile
    {
        private readonly IVertexFactory<Vertex> vertexFactory;

        public VerticesMappingProfile(IVertexFactory<Vertex> vertexFactory)
        {
            this.vertexFactory = vertexFactory;

            CreateMap<ICoordinate, ICoordinate>().ConvertUsing(x => x);
            CreateMap<VertexReadDto, Vertex>()
                .ConstructUsing(x => vertexFactory.CreateVertex(x.Coordinate));
            CreateMap<Vertex, VertexEntity>()
                .ForMember(x => x.X, opt => opt.MapFrom(x => x.Position.GetX()))
                .ForMember(x => x.Y, opt => opt.MapFrom(x => x.Position.GetY()))
                .ForMember(x => x.UpperValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.UpperValueOfRange))
                .ForMember(x => x.LowerValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.LowerValueOfRange))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => x.Cost.CurrentCost));
            CreateMap<VertexEntity, Vertex>().ConstructUsing(x => vertexFactory.CreateVertex(new Coordinate(x.X, x.Y)))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<VertexEntity, VertexReadDto>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => new Coordinate(x.X, x.Y)))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<Vertex, VertexSerializationDto>()
                .ForMember(x => x.Neighbors, opt => opt.MapFrom(x => x.Neighbours.GetCoordinates().ToReadOnly()));
            CreateMap<VertexSerializationDto, Vertex>().ConstructUsing(x => vertexFactory.CreateVertex(x.Position));
            CreateMap<IEnumerable<VertexSerializationDto>, IEnumerable<Vertex>>()
                .ConstructUsing((x, context) => ToVertices(x, context));
        }

        private IEnumerable<Vertex> ToVertices(IEnumerable<VertexSerializationDto> dtos, 
            ResolutionContext context)
        {
            var vertices = dtos
                .Select(context.Mapper.Map<Vertex>)
                .ToDictionary(x => x.Position);
            foreach (var dto in dtos)
            {
                var vertex = vertices[dto.Position];
                var neighbors = dto.Neighbors.Select(x => vertices[x]);
                vertex.Neighbours.AddRange(neighbors);
            }
            return vertices.Values;
        }
    }
}