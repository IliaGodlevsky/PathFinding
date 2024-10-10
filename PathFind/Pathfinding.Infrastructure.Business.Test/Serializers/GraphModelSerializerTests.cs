using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Shared.Extensions;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.Infrastructure.Business.Test.Serializers
{
    [TestFixture, UnitTest]
    public class GraphModelSerializerTests
    {
        [Test]
        public async Task SerializeToStream_ValidModel_ShouldSerialize()
        {
            var serializer = new JsonSerializer<GraphSerializationModel>();
            var model = EntityBuilder.CreateGraphSerializationModelFull();

            using var stream = new MemoryStream();
            await serializer.SerializeToAsync(model, stream);

            Assert.That(stream.Length, Is.GreaterThan(0));
        }

        [Test]
        public async Task SerializeToStream_ModelWithNullProps_ShouldSerialize()
        {
            var serializer = new JsonSerializer<GraphSerializationModel>();
            var model = new GraphSerializationModel();

            using var stream = new MemoryStream();
            await serializer.SerializeToAsync(model, stream);

            Assert.That(stream.Length, Is.GreaterThan(0));
        }

        [Test]
        public async Task DeserializeToStream_ModelWithNullProps_ShouldSerialize()
        {
            var serializer = new JsonSerializer<GraphSerializationModel>();
            var model = new GraphSerializationModel();

            using var stream = new MemoryStream();
            await serializer.SerializeToAsync(model, stream);
            stream.Seek(0, SeekOrigin.Begin);
            var result = await serializer.DeserializeFromAsync(stream);

            Assert.Multiple(() =>
            {
                Assert.That(result.Vertices is null, Is.True);
                Assert.That(result.DimensionSizes is null, Is.True);
                Assert.That(result.Name is null, Is.True);
                Assert.That(result.Neighborhood is null, Is.True);
                Assert.That(result.SmoothLevel is null, Is.True);
            });
        }

        [Test]
        public async Task DeserializeFromStream_ValidModel_ShouldDeserialize()
        {
            var serializer = new JsonSerializer<GraphSerializationModel>();
            var model = EntityBuilder.CreateGraphSerializationModelFull();

            using var stream = new MemoryStream();
            await serializer.SerializeToAsync(model, stream);
            stream.Seek(0, SeekOrigin.Begin);
            var result = await serializer.DeserializeFromAsync(stream);

            Assert.Multiple(() =>
            {
                Assert.That(stream.Length > 0, Is.True);
                Assert.That(result.Vertices.Count == model.Vertices.Count, Is.True);
                Assert.That(result.Vertices.Juxtapose(model.Vertices, (x, y) =>
                {
                    return x.IsObstacle == y.IsObstacle
                        && x.Neighbors.Juxtapose(y.Neighbors,
                            (i, j) => i.Coordinate.SequenceEqual(j.Coordinate))
                        && x.Cost.Cost == y.Cost.Cost
                        && x.Cost.UpperValueOfRange == y.Cost.UpperValueOfRange
                        && x.Cost.LowerValueOfRange == y.Cost.LowerValueOfRange
                        && x.Position.Coordinate.SequenceEqual(y.Position.Coordinate);
                }), Is.True);
                Assert.That(result.DimensionSizes.SequenceEqual(model.DimensionSizes), Is.True);
                Assert.That(result.Name == model.Name, Is.True);
                Assert.That(result.SmoothLevel == model.SmoothLevel, Is.True);
                Assert.That(result.Neighborhood == model.Neighborhood, Is.True);
            });
        }
    }
}
