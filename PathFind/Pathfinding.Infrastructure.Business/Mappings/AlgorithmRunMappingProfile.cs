using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Requests.Create;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class AlgorithmRunMappingProfile : Profile
    {
        public AlgorithmRunMappingProfile()
        {
            CreateMap<CreateAlgorithmRunRequest, AlgorithmRunModel>();
            CreateMap<AlgorithmRunModel, CreateAlgorithmRunRequest>();

            CreateMap<CreateAlgorithmRunRequest, AlgorithmRun>();
            CreateMap<AlgorithmRun, AlgorithmRunModel>();

            CreateMap<AlgorithmRunModel, AlgorithmRunSerializationModel>();
            CreateMap<AlgorithmRunSerializationModel, CreateAlgorithmRunRequest>();
            CreateMap<AlgorithmRunSerializationModel, AlgorithmRunModel>();

            CreateMap<AlgorithmRunHistoryModel, AlgorithmRunHistorySerializationModel>();
            CreateMap<AlgorithmRunHistorySerializationModel, CreateAlgorithmRunHistoryRequest>();
            CreateMap<AlgorithmRunHistorySerializationModel, AlgorithmRunHistoryModel>();
            CreateMap<AlgorithmRunHistoryModel, CreateAlgorithmRunHistoryRequest>();
            CreateMap<CreateAlgorithmRunHistoryRequest, AlgorithmRunHistoryModel>();
        }
    }
}
