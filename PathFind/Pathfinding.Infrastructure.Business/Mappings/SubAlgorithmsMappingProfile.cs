using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    public sealed class SubAlgorithmsMappingProfile : Profile
    {
        public SubAlgorithmsMappingProfile()
        {
            CreateMap<CreateSubAlgorithmRequest, SubAlgorithmModel>();
            CreateMap<SubAlgorithmModel, CreateSubAlgorithmRequest>();

            CreateMap<SubAlgorithm, SubAlgorithmModel>();
            CreateMap<CreateSubAlgorithmRequest, SubAlgorithm>();
            CreateMap<SubAlgorithmModel, SubAlgorithmSerializationModel>();
            CreateMap<SubAlgorithmSerializationModel, SubAlgorithmModel>();
            CreateMap<SubAlgorithmSerializationModel, CreateSubAlgorithmRequest>();

            CreateMap<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued), VisitedVerticesModel>()
                .ConvertUsing((x, y, context) => new VisitedVerticesModel()
                {
                    Current = context.Mapper.Map<CoordinateModel>(x.Visited),
                    Enqueued = context.Mapper.Map<CoordinateModel[]>(x.Enqueued)
                });
            CreateMap<VisitedVerticesModel, (Coordinate, IReadOnlyList<Coordinate>)>()
                .ConvertUsing((x, y, context)
                => (context.Mapper.Map<Coordinate>(x.Current),
                    context.Mapper.Map<Coordinate[]>(x.Enqueued).ToReadOnly()));
        }
    }
}
