﻿using AutoMapper;
using Pathfinding.Infrastructure.Business.Mappings;
using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations
{
    internal sealed class TestMapper : Singleton<TestMapper, TestMapper>
    {
        public IMapper Mapper { get; }

        private TestMapper()
        {
            var visitedVerticesSerializer = new JsonSerializer<IEnumerable<VisitedVerticesModel>>();
            var coordinateModelSerializer = new JsonSerializer<IEnumerable<CoordinateModel>>();
            var coordinateCostPairSerializer = new JsonSerializer<IEnumerable<CostCoordinatePair>>();

            var profiles = new Profile[]
            {
                new UntitledMappingProfile(coordinateModelSerializer,
                    visitedVerticesSerializer,
                    coordinateCostPairSerializer),
                new SubAlgorithmsMappingProfile(),
                new StatisticsMappingProfile(),
                new HistoryMappingProfile<TestVertex>(),
                new GraphStateMappingProfile(),
                new VerticesMappingProfile<TestVertex>(new TestVertexFactory()),
                new GraphMappingProfile<TestVertex>(new GraphFactory<TestVertex>()),
                new AlgorithmRunMappingProfile()
            };

            var config = new MapperConfiguration(c => c.AddProfiles(profiles));
            Mapper = config.CreateMapper();
        }
    }
}
