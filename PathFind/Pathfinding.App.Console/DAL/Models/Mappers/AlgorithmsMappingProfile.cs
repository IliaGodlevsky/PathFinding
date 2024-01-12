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
        private readonly ISerializer<IEnumerable<CoordinateDto>> coordinateSerializer;
        private readonly ISerializer<IEnumerable<int>> arraySerializer;

        public AlgorithmsMappingProfile(ISerializer<IEnumerable<CoordinateDto>> coordinateSerializer,
            ISerializer<IEnumerable<int>> arraySerializer)
        {
            this.coordinateSerializer = coordinateSerializer;
            this.arraySerializer = arraySerializer;

            CreateMap<byte[], IReadOnlyCollection<ICoordinate>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<ICoordinate[]>(this.coordinateSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<ICoordinate>, byte[]>()
                .ConvertUsing((x, y, context) => this.coordinateSerializer.SerializeToBytes(context.Mapper.Map<CoordinateDto[]>(x)));
            CreateMap<AlgorithmEntity, AlgorithmReadDto>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => FromBytesToCosts(x.Costs)))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => JsonConvert.DeserializeObject<Statistics>(x.Statistics)));
            CreateMap<AlgorithmCreateDto, AlgorithmEntity>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => FromCostsToBytes(x.Costs)))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => JsonConvert.SerializeObject(x.Statistics)));
            CreateMap<AlgorithmReadDto, AlgorithmSerializationDto>();
            CreateMap<AlgorithmSerializationDto, AlgorithmCreateDto>();
            CreateMap<AlgorithmCreateDto, AlgorithmReadDto>();
        }

        private IReadOnlyCollection<int> FromBytesToCosts(byte[] str)
        {
            return arraySerializer.DeserializeFromBytes(str).ToReadOnly();
        }

        private byte[] FromCostsToBytes(IEnumerable<int> costs)
        {
            return arraySerializer.SerializeToBytes(costs);
        }
    }
}
