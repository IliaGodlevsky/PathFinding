using AutoMapper;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class UntitledMappingConfig : Profile
    {
        public UntitledMappingConfig(
            ISerializer<IEnumerable<CoordinateDto>> coordinateSerializer,
            ISerializer<IEnumerable<int>> arraySerializer,
            ISerializer<IEnumerable<VisitedVerticesDto>> visitedVerticesSerializer)
        {
            CreateMap<byte[], IReadOnlyCollection<ICoordinate>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<ICoordinate[]>(coordinateSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<ICoordinate>, byte[]>()
                .ConvertUsing((x, y, context) => coordinateSerializer.SerializeToBytes(context.Mapper.Map<CoordinateDto[]>(x)));
            CreateMap<ICoordinate, CoordinateDto>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => x.ToReadOnly()));
            CreateMap<CoordinateDto, ICoordinate>()
                .ConvertUsing(x => new Coordinate(x.Coordinate.ToArray()));
            CreateMap<ICoordinate, ICoordinate>().ConvertUsing(x => x);
            CreateMap<byte[], IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)>>(visitedVerticesSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)>, byte[]>()
                .ConvertUsing((x, y, context) => visitedVerticesSerializer.SerializeToBytes(context.Mapper.Map<VisitedVerticesDto[]>(x)));
        }
    }
}
