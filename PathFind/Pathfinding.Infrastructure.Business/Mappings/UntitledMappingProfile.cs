using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Read;
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
            ISerializer<IEnumerable<VisitedVerticesModel>> visitedVerticesSerializer,
            ISerializer<IEnumerable<CostCoordinatePair>> coordinateCostSerializer)
        {
            CreateMap<IReadOnlyCollection<(Coordinate Position, int Cost)>, IReadOnlyCollection<CostCoordinatePair>>()
                .ConvertUsing((x, y, context) =>
                {
                    return x.Select(i => new CostCoordinatePair()
                    {
                        Position = context.Mapper.Map<CoordinateModel>(i.Position),
                        Cost = i.Cost
                    }).ToArray();
                });
            CreateMap<IReadOnlyCollection<CostCoordinatePair>, IReadOnlyCollection<(Coordinate Position, int Cost)>>()
                .ConvertUsing((x, y, context) =>
                {
                    return x.Select(i => (Position: context.Mapper.Map<Coordinate>(i.Position), Cost: i.Cost)).ToArray();
                });
            CreateMap<byte[], IReadOnlyCollection<(Coordinate, int)>>()
                .ConvertUsing((x, y, context) => FromBytes(coordinateCostSerializer, context, x));
            CreateMap<IReadOnlyCollection<(Coordinate, int)>, byte[]>()
                .ConvertUsing((x, y, context) => ToBytes(coordinateCostSerializer, context, x));
            CreateMap<byte[], Coordinate>()
                .ConstructUsing((x, context) => new Coordinate(context.Mapper.Map<IReadOnlyCollection<int>>(x).ToArray()));
            CreateMap<byte[], IReadOnlyCollection<int>>().ConvertUsing(x => FromBytes(intArraySerializer, x));
            CreateMap<IReadOnlyCollection<int>, byte[]>().ConvertUsing(x => ToBytes(intArraySerializer, x));
            CreateMap<Coordinate, byte[]>().ConvertUsing((x, y, context) => context.Mapper.Map<byte[]>(x.CoordinatesValues));
            CreateMap<byte[], IReadOnlyCollection<Coordinate>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<Coordinate[]>(coordinateSerializer.DeserializeFromBytes(x).Result).ToReadOnly());
            CreateMap<IReadOnlyCollection<Coordinate>, byte[]>()
                .ConvertUsing((x, y, context) => coordinateSerializer.SerializeToBytesAsync(context.Mapper.Map<CoordinateModel[]>(x)).Result);
            CreateMap<Coordinate, CoordinateModel>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => x.CoordinatesValues.ToReadOnly()));
            CreateMap<CoordinateModel, Coordinate>()
                .ConvertUsing(x => new Coordinate(x.Coordinate.ToArray()));
            CreateMap<PathfindingRange, PathfindingRangeModel>();
            CreateMap<Coordinate, Coordinate>().ConvertUsing(x => x);
            CreateMap<byte[], IReadOnlyCollection<(Coordinate, IReadOnlyList<Coordinate>)>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<IReadOnlyCollection<(Coordinate, IReadOnlyList<Coordinate>)>>(visitedVerticesSerializer.DeserializeFromBytes(x).Result).ToReadOnly());
            CreateMap<IReadOnlyCollection<(Coordinate, IReadOnlyList<Coordinate>)>, byte[]>()
                .ConvertUsing((x, y, context) => visitedVerticesSerializer.SerializeToBytesAsync(context.Mapper.Map<VisitedVerticesModel[]>(x)).Result);
        }

        private byte[] ToBytes(ISerializer<IEnumerable<CostCoordinatePair>> coordinateCostSerializer,
            ResolutionContext context,
            IReadOnlyCollection<(Coordinate Position, int Cost)> costCoordinatePair)
        {
            var pairs = context.Mapper.Map<CostCoordinatePair[]>(costCoordinatePair);
            return coordinateCostSerializer.SerializeToBytesAsync(pairs).GetAwaiter().GetResult();
        }

        private IReadOnlyCollection<(Coordinate, int)> FromBytes(ISerializer<IEnumerable<CostCoordinatePair>> coordinateCostSerializer,
            ResolutionContext context,
            byte[] costCoordinatePair)
        {
            var result = coordinateCostSerializer.DeserializeFromBytes(costCoordinatePair).GetAwaiter().GetResult();
            return context.Mapper
                .Map<IReadOnlyCollection<(Coordinate, int)>>(result).ToArray();
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
