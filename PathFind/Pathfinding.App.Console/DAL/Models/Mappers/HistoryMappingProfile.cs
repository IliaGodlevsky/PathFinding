using AutoMapper;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;

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
