using AutoMapper;
using Pathfinding.Infrastructure.Business.Test.TestRealizations;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Primitives;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.Infrastructure.Business.Test.Mappings
{
    [TestFixture, UnitTest]
    public class UntitledMappingProfileTests
    {
        private readonly IMapper mapper;

        public UntitledMappingProfileTests()
        {
            mapper = TestMapper.Interface.Mapper;
        }

        [TestCaseSource(typeof(MappingTestsDataProvider), nameof(MappingTestsDataProvider.Coordinates))]
        public void UntitledMapperProfile_CoordinatesToCoordinateModel_ShouldMap(int[] coordinateValues)
        {
            var coordinate = new Coordinate(coordinateValues);

            var mapped = mapper.Map<CoordinateModel>(coordinate);

            Assert.That(coordinate.CoordinatesValues.SequenceEqual(mapped.Coordinate), Is.True);
        }

        [TestCaseSource(typeof(MappingTestsDataProvider), nameof(MappingTestsDataProvider.Coordinates))]
        public void UntitledMapperProfile_CoordinateModelToCoordinates_ShouldMap(int[] coordinateValues)
        {
            var coordinateModel = new CoordinateModel() { Coordinate = coordinateValues };

            var mapped = mapper.Map<Coordinate>(coordinateModel);

            Assert.That(coordinateModel.Coordinate.SequenceEqual(mapped.CoordinatesValues), Is.True);
        }

        [TestCaseSource(typeof(MappingTestsDataProvider), nameof(MappingTestsDataProvider.CoordinateValuesToBytes))]
        public byte[] UntitledMapperProfile_CoordinateValuesToByte_ShouldMap(int[] coordinateValues)
        {
            return mapper.Map<byte[]>(coordinateValues);
        }

        [TestCaseSource(typeof(MappingTestsDataProvider), nameof(MappingTestsDataProvider.CoordinateToBytes))]
        public byte[] UntitledMapperProfile_CoordinateToBytes_ShouldMap(Coordinate coordinate)
        {
            return mapper.Map<byte[]>(coordinate);
        }
    }
}
