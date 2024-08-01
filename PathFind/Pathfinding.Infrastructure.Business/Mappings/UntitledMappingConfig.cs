using AutoMapper;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Undefined;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class UntitledMappingConfig : Profile
    {
        public UntitledMappingConfig(
            ISerializer<IEnumerable<CoordinateModel>> coordinateSerializer,
            ISerializer<IEnumerable<int>> intArraySerializer,
            ISerializer<IEnumerable<VisitedVerticesModel>> visitedVerticesSerializer)
        {
            CreateMap<byte[], Coordinate>()
                .ConstructUsing((x, context) => new Coordinate(context.Mapper.Map<IReadOnlyCollection<int>>(x).ToArray()));
            CreateMap<byte[], IReadOnlyCollection<int>>().ConvertUsing(x => FromBytes(intArraySerializer, x));
            CreateMap<IReadOnlyCollection<int>, byte[]>().ConvertUsing(x => ToBytes(intArraySerializer, x));
            CreateMap<byte[], IReadOnlyCollection<ICoordinate>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<ICoordinate[]>(coordinateSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<ICoordinate>, byte[]>()
                .ConvertUsing((x, y, context) => coordinateSerializer.SerializeToBytesAsync(context.Mapper.Map<CoordinateModel[]>(x)).Result);
            CreateMap<ICoordinate, CoordinateModel>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => x.ToReadOnly()));
            CreateMap<CoordinateModel, ICoordinate>()
                .ConvertUsing(x => new Coordinate(x.Coordinate.ToArray()));
            CreateMap<ICoordinate, ICoordinate>().ConvertUsing(x => x);
            CreateMap<byte[], IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)>>(visitedVerticesSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)>, byte[]>()
                .ConvertUsing((x, y, context) => visitedVerticesSerializer.SerializeToBytesAsync(context.Mapper.Map<VisitedVerticesModel[]>(x)).Result);
        }

        private byte[] ToBytes(ISerializer<IEnumerable<int>> intArraySerializer,
            IReadOnlyCollection<int> coordinate)
        {
            return intArraySerializer.SerializeToBytesAsync(coordinate).Result;
        }

        private IReadOnlyCollection<int> FromBytes(ISerializer<IEnumerable<int>> intArraySerializer, byte[] bytes)
        {
            return intArraySerializer.DeserializeFromBytes(bytes).Result.ToReadOnly();
        }
    }
}
