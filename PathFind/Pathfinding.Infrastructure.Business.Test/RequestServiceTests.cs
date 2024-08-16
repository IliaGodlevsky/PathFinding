using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Mappers;
using Pathfinding.Infrastructure.Business.Mappings;
using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Infrastructure.Business.Test.Mock;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Service.Interface.Models.Undefined;

namespace Pathfinding.Infrastructure.Business.Test
{
    [TestFixture]
    public class RequestServiceTests
    {
        private RequestService<TestVertex> service;

        [SetUp]
        public void SetUp()
        {
            var visitedVerticesSerializer = new JsonSerializer<IEnumerable<VisitedVerticesModel>>();
            var coordinateModelSerializer = new JsonSerializer<IEnumerable<CoordinateModel>>();
            var intArraySerializer = new JsonSerializer<IEnumerable<int>>();

            var profiles = new Profile[]
            {
                new UntitledMappingProfile(coordinateModelSerializer,
                    intArraySerializer,
                    visitedVerticesSerializer),
                new SubAlgorithmsMappingProfile(),
                new StatisticsMappingProfile(),
                new HistoryMappingProfile<TestVertex>(),
                new GraphStateMappingProfile(),
                new VerticesMappingProfile<TestVertex>(new TestVertexFactory()),
                new GraphMappingProfile<TestVertex>(new GraphFactory<TestVertex>()),
                new AlgorithmRunMappingProfile()
            };

            var config = new MapperConfiguration(c => c.AddProfiles(profiles));
            var mapper = config.CreateMapper();
            service = new RequestService<TestVertex>(mapper);
        }
    }
}
