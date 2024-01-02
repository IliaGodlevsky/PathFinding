using AutoMapper;
using Pathfinding.App.Console.DAL.Models.TransferObjects;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class HistoryMappingProfile : Profile
    {
        public HistoryMappingProfile()
        {
            CreateMap<PathfindingHistoryReadDto, PathfindingHistorySerializationDto>();
            CreateMap<PathfindingHistorySerializationDto, PathfindingHistoryCreateDto>();
            CreateMap<PathfindingHistoryCreateDto, PathfindingHistoryReadDto>();
        }
    }
}
