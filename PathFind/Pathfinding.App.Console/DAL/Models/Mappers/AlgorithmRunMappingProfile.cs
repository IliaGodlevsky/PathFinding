using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class AlgorithmRunMappingProfile : Profile
    {
        public AlgorithmRunMappingProfile()
        {
            CreateMap<AlgorithmRunCreateDto, AlgorithmRunReadDto>();
            CreateMap<AlgorithmRunReadDto, AlgorithmRunCreateDto>();

            CreateMap<AlgorithmRunCreateDto, AlgorithmRunEntity>();
            CreateMap<AlgorithmRunEntity, AlgorithmRunReadDto>();

            CreateMap<AlgorithmRunReadDto, AlgorithmRunSerializationDto>();
            CreateMap<AlgorithmRunSerializationDto, AlgorithmRunCreateDto>();
            CreateMap<AlgorithmRunSerializationDto, AlgorithmRunReadDto>();

            CreateMap<AlgorithmRunHistoryReadDto, AlgorithmRunHistorySerializationDto>();
            CreateMap<AlgorithmRunHistorySerializationDto, AlgorithmRunHistoryCreateDto>();
            CreateMap<AlgorithmRunHistorySerializationDto, AlgorithmRunHistoryReadDto>();
            CreateMap<AlgorithmRunHistoryReadDto, AlgorithmRunHistoryCreateDto>();
            CreateMap<AlgorithmRunHistoryCreateDto, AlgorithmRunHistoryReadDto>();
        }
    }
}
