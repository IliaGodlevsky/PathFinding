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
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Contexts;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class AlgorithmsMappingProfile : Profile
    {
        public AlgorithmsMappingProfile(
            ISerializer<IEnumerable<CoordinateDto>> coordinateSerializer,
            ISerializer<IEnumerable<VisitedVerticesDto>> visitedVerticesSerializer,
            ISerializer<IEnumerable<int>> arraySerializer)
        {
            CreateMap<byte[], IReadOnlyCollection<ICoordinate>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<ICoordinate[]>(coordinateSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<ICoordinate>, byte[]>()
                .ConvertUsing((x, y, context) => coordinateSerializer.SerializeToBytes(context.Mapper.Map<CoordinateDto[]>(x)));
            CreateMap<byte[], IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)>>(visitedVerticesSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)>, byte[]>()
                .ConvertUsing((x, y, context) => visitedVerticesSerializer.SerializeToBytes(context.Mapper.Map<VisitedVerticesDto[]>(x)));
            CreateMap<AlgorithmEntity, AlgorithmReadDto>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => arraySerializer.DeserializeFromBytes(x.Costs).ToReadOnly()))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => JsonConvert.DeserializeObject<Statistics>(x.Statistics)));
            CreateMap<AlgorithmCreateDto, AlgorithmEntity>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => arraySerializer.SerializeToBytes(x.Costs)))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => JsonConvert.SerializeObject(x.Statistics)));
            CreateMap<AlgorithmReadDto, AlgorithmSerializationDto>();
            CreateMap<AlgorithmSerializationDto, AlgorithmCreateDto>();
            CreateMap<AlgorithmCreateDto, AlgorithmReadDto>();
            CreateMap<(ICoordinate, IReadOnlyList<ICoordinate>), VisitedVerticesDto>()
                .ConvertUsing((x, y, context) => new VisitedVerticesDto() 
                { 
                    Current = context.Mapper.Map<CoordinateDto>(x.Item1),
                    Enqueued = context.Mapper.Map<CoordinateDto[]>(x.Item2) 
                });
            CreateMap<VisitedVerticesDto, (ICoordinate, IReadOnlyList<ICoordinate>)>()
                .ConvertUsing((x, y, context)
                => (context.Mapper.Map<ICoordinate>(x.Current), 
                    context.Mapper.Map<ICoordinate[]>(x.Enqueued).ToReadOnly()));
        }
    }
}
