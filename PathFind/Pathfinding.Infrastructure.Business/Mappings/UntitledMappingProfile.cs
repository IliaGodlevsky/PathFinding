using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class UntitledMappingProfile : Profile
    {
        public UntitledMappingProfile()
        {
            CreateMap<Coordinate, CoordinateModel>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => x.CoordinatesValues.ToReadOnly()));
            CreateMap<CoordinateModel, Coordinate>()
                .ConvertUsing(x => new Coordinate(x.Coordinate.ToArray()));
            CreateMap<PathfindingRange, PathfindingRangeModel>();
            CreateMap<Coordinate, Coordinate>().ConvertUsing(x => x);
        }
    }
}
