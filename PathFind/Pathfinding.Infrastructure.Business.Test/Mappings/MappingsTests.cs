using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Mappers;
using Pathfinding.Infrastructure.Business.Mappings;
using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Infrastructure.Business.Test.Mock;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Test.Mappings
{
    [TestFixture]
    public class MappingsTests
    {
        private readonly IMapper mapper;

        public MappingsTests()
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
            mapper = config.CreateMapper();
        }

        [TestCaseSource(typeof(CoordinatesDataProvider), nameof(CoordinatesDataProvider.Coordinates))]
        public void UntitledMapperProfile_CoordinatesToCoordinateModel_ShouldMap(int[] coordinateValues)
        {
            var coordinate = new Coordinate(coordinateValues);

            var mapped = mapper.Map<CoordinateModel>(coordinate);

            Assert.IsTrue(coordinate.CoordinatesValues.SequenceEqual(mapped.Coordinate));
        }

        [TestCaseSource(typeof(CoordinatesDataProvider), nameof(CoordinatesDataProvider.Coordinates))]
        public void UntitledMapperProfile_CoordinateModelToCoordinates_ShouldMap(int[] coordinateValues)
        {
            var coordinateModel = new CoordinateModel() { Coordinate = coordinateValues };

            var mapped = mapper.Map<Coordinate>(coordinateModel);

            Assert.IsTrue(coordinateModel.Coordinate.SequenceEqual(mapped.CoordinatesValues));
        }

        [TestCaseSource(typeof(CoordinatesDataProvider), nameof(CoordinatesDataProvider.CoordinateValuesToBytes))]
        public byte[] UntitledMapperProfile_CoordinateValuesToByte_ShouldMap(int[] coordinateValues)
        {
            var mapped = mapper.Map<byte[]>(coordinateValues);

            return mapped;
        }

        [TestCaseSource(typeof(CoordinatesDataProvider), nameof(CoordinatesDataProvider.CoordinateToBytes))]
        public byte[] UntitledMapperProfile_CoordinateToBytes_ShouldMap(Coordinate coordinate)
        {
            return mapper.Map<byte[]>(coordinate);
        }
    }
}
