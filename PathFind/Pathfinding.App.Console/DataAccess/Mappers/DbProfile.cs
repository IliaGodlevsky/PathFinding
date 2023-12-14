using AutoMapper;
using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Pathfinding.App.Console.Model.Notes;
using System;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.App.Console.DataAccess.Mappers
{
    internal sealed class DbProfile : Profile
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> coordinateSerializer;
        private readonly ISerializer<IEnumerable<int>> arraySerializer;
        private readonly IVertexFactory<Vertex> vertexFactory;

        public DbProfile(ISerializer<IEnumerable<ICoordinate>> coordinateSerializer,
            ISerializer<IEnumerable<int>> arraySerializer,
            IVertexFactory<Vertex> vertexFactory)
        {
            this.coordinateSerializer = new CompressSerializer<IEnumerable<ICoordinate>>(coordinateSerializer);
            this.arraySerializer = new CompressSerializer<IEnumerable<int>>(arraySerializer);
            this.vertexFactory = vertexFactory;

            CreateMap<AlgorithmEntity, AlgorithmReadDto>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => FromStringToCosts(x.Costs)))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => FromStringToStatistics(x.Statistics)))
                .ForMember(x => x.Path, opt => opt.MapFrom(x => FromStringToCoordinates(x.Path)))
                .ForMember(x => x.Obstacles, opt => opt.MapFrom(x => FromStringToCoordinates(x.Obstacles)))
                .ForMember(x => x.Visited, opt => opt.MapFrom(x => FromStringToCoordinates(x.Visited)))
                .ForMember(x => x.Range, opt => opt.MapFrom(x => FromStringToCoordinates(x.Range)));

            CreateMap<AlgorithmCreateDto, AlgorithmEntity>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => FromCostsToString(x.Costs)))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => FromStatisticsToString(x.Statistics)))
                .ForMember(x => x.Path, opt => opt.MapFrom(x => FromCoordinatesToString(x.Path)))
                .ForMember(x => x.Obstacles, opt => opt.MapFrom(x => FromCoordinatesToString(x.Obstacles)))
                .ForMember(x => x.Visited, opt => opt.MapFrom(x => FromCoordinatesToString(x.Visited)))
                .ForMember(x => x.Range, opt => opt.MapFrom(x => FromCoordinatesToString(x.Range)));

            CreateMap<ICoordinate, ICoordinate>().ConvertUsing(x => x);
            CreateMap<VertexReadDto, Vertex>().ConstructUsing(x => vertexFactory.CreateVertex(x.Coordinate));

            CreateMap<Vertex, VertexEntity>()
                .ForMember(x => x.X, opt => opt.MapFrom(x => x.Position.GetX()))
                .ForMember(x => x.Y, opt => opt.MapFrom(x => x.Position.GetY()))
                .ForMember(x => x.UpperValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.UpperValueOfRange))
                .ForMember(x => x.LowerValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.LowerValueOfRange))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => x.Cost.CurrentCost));

            CreateMap<VertexEntity, VertexReadDto>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => new Coordinate(x.X, x.Y)))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));

            CreateMap<IGraph<Vertex>, IGraph<Vertex>>().ConvertUsing(x => x);
            CreateMap<AlgorithmReadDto, AlgorithmSerializationDto>();
            CreateMap<AlgorithmSerializationDto, AlgorithmCreateDto>();
            CreateMap<PathfindingHistoryReadDto, PathfindingHistorySerializationDto>();
            CreateMap<PathfindingHistorySerializationDto, PathfindingHistoryCreateDto>();
            CreateMap<AlgorithmCreateDto, AlgorithmReadDto>();
        }

        private IReadOnlyCollection<int> FromStringToCosts(string str)
        {
            return Array.AsReadOnly(arraySerializer.DeserializeFromString(str).ToArray());
        }

        private IReadOnlyCollection<ICoordinate> FromStringToCoordinates(string str)
        {
            return Array.AsReadOnly(coordinateSerializer.DeserializeFromString(str).ToArray());
        }

        private Statistics FromStringToStatistics(string str)
        {
            return JsonConvert.DeserializeObject<Statistics>(str);
        }

        private string FromCostsToString(IEnumerable<int> costs)
        {
            return arraySerializer.SerializeToString(costs);
        }

        private string FromCoordinatesToString(IEnumerable<ICoordinate> coordinates)
        {
            return coordinateSerializer.SerializeToString(coordinates);
        }

        private string FromStatisticsToString(Statistics statistics)
        {
            return JsonConvert.SerializeObject(statistics);
        }
    }
}
