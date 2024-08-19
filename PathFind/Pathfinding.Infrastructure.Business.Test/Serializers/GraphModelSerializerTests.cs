using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Infrastructure.Business.Test.Mock;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Test.Serializers
{
    [TestFixture]
    public class GraphModelSerializerTests
    {
        [Test]
        public async Task SerializeToStream_ValidModel_ShouldSerialize()
        {
            var serializer = new JsonSerializer<GraphSerializationModel>();
            var model = new GraphSerializationModel()
            {
                DimensionSizes = TestGraph.Interface.DimensionsSizes,
                Name = "test",
                Vertices = TestMapper.Instance.Mapper
                    .Map<VertexSerializationModel[]>(TestGraph.Interface.ToArray())
            };

            using var stream = new MemoryStream();
            await serializer.SerializeToAsync(model, stream);
            stream.Seek(0, SeekOrigin.Begin);
            var result = await serializer.DeserializeFromAsync(stream);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(stream.Length > 0);
                Assert.IsTrue(result.Vertices.Count == model.Vertices.Count);
                Assert.IsTrue(result.Vertices.Juxtapose(model.Vertices, (x, y) =>
                {
                    return x.IsObstacle == y.IsObstacle
                        && x.Neighbors.Juxtapose(y.Neighbors, 
                            (i, j) => i.Coordinate.SequenceEqual(j.Coordinate))
                        && x.Cost.Cost == y.Cost.Cost
                        && x.Cost.UpperValueOfRange == y.Cost.UpperValueOfRange
                        && x.Cost.LowerValueOfRange == y.Cost.LowerValueOfRange
                        && x.Position.Coordinate.SequenceEqual(y.Position.Coordinate);
                }));
                Assert.IsTrue(result.DimensionSizes.SequenceEqual(model.DimensionSizes));
                Assert.IsTrue(result.Name == model.Name);
            });
        }
    }
}
