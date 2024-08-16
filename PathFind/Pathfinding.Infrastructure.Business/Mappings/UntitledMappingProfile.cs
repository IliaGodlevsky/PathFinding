using AutoMapper;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class UntitledMappingProfile : Profile
    {
        public UntitledMappingProfile(
            ISerializer<IEnumerable<CoordinateModel>> coordinateSerializer,
            ISerializer<IEnumerable<int>> intArraySerializer,
            ISerializer<IEnumerable<VisitedVerticesModel>> visitedVerticesSerializer)
        {
            CreateMap<byte[], Coordinate>()
                .ConstructUsing((x, context) => new Coordinate(context.Mapper.Map<IReadOnlyCollection<int>>(x).ToArray()));
            CreateMap<byte[], IReadOnlyCollection<int>>().ConvertUsing(x => FromBytes(intArraySerializer, x));
            CreateMap<IReadOnlyCollection<int>, byte[]>().ConvertUsing(x => ToBytes(intArraySerializer, x));
            CreateMap<Coordinate, byte[]>().ConvertUsing((x, y, context) => context.Mapper.Map<byte[]>(x.CoordinatesValues));
            CreateMap<byte[], IReadOnlyCollection<Coordinate>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<Coordinate[]>(coordinateSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<Coordinate>, byte[]>()
                .ConvertUsing((x, y, context) => coordinateSerializer.SerializeToBytesAsync(context.Mapper.Map<CoordinateModel[]>(x)).Result);
            CreateMap<Coordinate, CoordinateModel>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => x.CoordinatesValues.ToReadOnly()));
            CreateMap<CoordinateModel, Coordinate>()
                .ConvertUsing(x => new Coordinate(x.Coordinate.ToArray()));
            CreateMap<Coordinate, Coordinate>().ConvertUsing(x => x);
            CreateMap<byte[], IReadOnlyCollection<(Coordinate, IReadOnlyList<Coordinate>)>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<IReadOnlyCollection<(Coordinate, IReadOnlyList<Coordinate>)>>(visitedVerticesSerializer.DeserializeFromBytes(x)).ToReadOnly());
            CreateMap<IReadOnlyCollection<(Coordinate, IReadOnlyList<Coordinate>)>, byte[]>()
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
