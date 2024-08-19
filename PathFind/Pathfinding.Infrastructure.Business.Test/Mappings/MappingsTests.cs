using AutoMapper;
using Pathfinding.Infrastructure.Business.Test.Mock;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Test.Mappings
{
    [TestFixture]
    public class MappingsTests
    {
        private readonly IMapper mapper;

        public MappingsTests()
        {
            mapper = TestMapper.Interface.Mapper;
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
