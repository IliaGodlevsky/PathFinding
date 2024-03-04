using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class SubAlgorithmsMappingProfile : Profile
    {
        public SubAlgorithmsMappingProfile() 
        {
            CreateMap<SubAlgorithmCreateDto, SubAlgorithmReadDto>();
            CreateMap<SubAlgorithmReadDto, SubAlgorithmCreateDto>();

            CreateMap<SubAlgorithmEntity, SubAlgorithmReadDto>();
            CreateMap<SubAlgorithmCreateDto, SubAlgorithmEntity>();
            CreateMap<SubAlgorithmReadDto, SubAlgorithmSerializationDto>();
            CreateMap<SubAlgorithmSerializationDto, SubAlgorithmCreateDto>();

            CreateMap<(ICoordinate Visited, IReadOnlyList<ICoordinate> Enqueued), VisitedVerticesDto>()
                .ConvertUsing((x, y, context) => new VisitedVerticesDto()
                {
                    Current = context.Mapper.Map<CoordinateDto>(x.Visited),
                    Enqueued = context.Mapper.Map<CoordinateDto[]>(x.Enqueued)
                });
            CreateMap<VisitedVerticesDto, (ICoordinate, IReadOnlyList<ICoordinate>)>()
                .ConvertUsing((x, y, context)
                => (context.Mapper.Map<ICoordinate>(x.Current),
                    context.Mapper.Map<ICoordinate[]>(x.Enqueued).ToReadOnly()));
        }
    }
}
