using AutoMapper;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class HistoryMappingProfile<T> : Profile
        where T : IVertex
    {
        public HistoryMappingProfile()
        {
            CreateMap<PathfindingHistoryReadDto<T>, PathfindingHistorySerializationDto>();
            CreateMap<PathfindingHistorySerializationDto, PathfindingHistoryCreateDto<T>>();
            CreateMap<PathfindingHistoryCreateDto<T>, PathfindingHistoryReadDto<T>>();
        }
    }
}
