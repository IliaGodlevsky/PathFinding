using AutoMapper;
using Newtonsoft.Json;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class AlgorithmsMappingProfile : Profile
    {
        private readonly CompressSerializer<IEnumerable<ICoordinate>> coordinateSerializer;
        private readonly CompressSerializer<IEnumerable<int>> arraySerializer;

        public AlgorithmsMappingProfile(ISerializer<IEnumerable<ICoordinate>> coordinateSerializer,
            ISerializer<IEnumerable<int>> arraySerializer)
        {
            this.coordinateSerializer = new(coordinateSerializer);
            this.arraySerializer = new(arraySerializer);

            CreateMap<byte[], IReadOnlyCollection<ICoordinate>>()
                .ConvertUsing(x => this.coordinateSerializer.DeserializeFromBytes(x).ToReadOnly());
            CreateMap<IReadOnlyCollection<ICoordinate>, byte[]>()
                .ConvertUsing(x => this.coordinateSerializer.SerializeToBytes(x));
            CreateMap<AlgorithmSerializationDto, AlgorithmJsonSerializationDto>();
            CreateMap<AlgorithmJsonSerializationDto, AlgorithmSerializationDto>();
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
