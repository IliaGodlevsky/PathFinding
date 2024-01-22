using AutoMapper;
using Newtonsoft.Json;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class AlgorithmsMappingProfile : Profile
    {
        public AlgorithmsMappingProfile(
            ISerializer<IEnumerable<CoordinateDto>> coordinateSerializer,
            ISerializer<IEnumerable<int>> arraySerializer)
        {
            CreateMap<byte[], IReadOnlyCollection<ICoordinate>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<ICoordinate[]>(coordinateSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<ICoordinate>, byte[]>()
                .ConvertUsing((x, y, context) => coordinateSerializer.SerializeToBytes(context.Mapper.Map<CoordinateDto[]>(x)));
            CreateMap<AlgorithmEntity, AlgorithmReadDto>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => arraySerializer.DeserializeFromBytes(x.Costs).ToReadOnly()))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => JsonConvert.DeserializeObject<Statistics>(x.Statistics)));
            CreateMap<AlgorithmCreateDto, AlgorithmEntity>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => arraySerializer.SerializeToBytes(x.Costs)))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => JsonConvert.SerializeObject(x.Statistics)));
            CreateMap<AlgorithmReadDto, AlgorithmSerializationDto>();
            CreateMap<AlgorithmSerializationDto, AlgorithmCreateDto>();
            CreateMap<AlgorithmCreateDto, AlgorithmReadDto>();
        }
    }
}
