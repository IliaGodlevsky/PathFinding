using Algorithm.Interfaces;
using Algorithm.Realizations.Tests.TestObjects;
using Common.Extensions;
using GraphLib.Common;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Realizations.Factories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.Tests
{
    public class GraphPathTests
    {
        private const int VerticesCount = 14;

        private Mock<IEndPoints> endPointsMock;

        private Mock<IVertexFactory> vertexFactoryMock;
        private Mock<IVertexCostFactory> vertexCostFactoryMock;
        private Mock<IGraphFactory> graphFactoryMock;
        private Mock<ICoordinateFactory> coordinateFactoryMock;
        private Mock<IVertexCost> vertexCostMock;
        private IGraphAssembler graphAssembler;

        public GraphPathTests()
        {
            vertexCostMock = new Mock<IVertexCost>();
            vertexCostMock.Setup(cost => cost.CurrentCost).Returns(1);

            vertexFactoryMock = new Mock<IVertexFactory>();
            vertexFactoryMock.Setup(vf => vf.CreateVertex()).Returns(new TestVertex());

            vertexCostFactoryMock = new Mock<IVertexCostFactory>();
            vertexCostFactoryMock.Setup(vcf => vcf.CreateCost()).Returns(vertexCostMock.Object);

            graphFactoryMock = new Mock<IGraphFactory>();
            graphFactoryMock.Setup(gf => gf.CreateGraph(It.IsAny<int[]>()))
                .Callback<int[]>(dimensions => new TestGraph(dimensions));

            coordinateFactoryMock = new Mock<ICoordinateFactory>();
            coordinateFactoryMock.Setup(cf => cf.CreateCoordinate(It.IsAny<IEnumerable<int>>()))
                .Callback<IEnumerable<int>>(coordinate => new TestCoordinate(coordinate.ToArray()));

            graphAssembler = new GraphAssembler(vertexFactoryMock.Object, 
                coordinateFactoryMock.Object, 
                graphFactoryMock.Object, 
                vertexCostFactoryMock.Object);
            

        }

        [Test]
        public void Path_NotEmptyDictionary_ReturnsValidPath()
        {
            var graph = graphAssembler.AssembleGraph(0, VerticesCount);
            endPointsMock = new Mock<IEndPoints>();
            endPointsMock.Setup(ep => ep.Start).Returns(graph.Vertices.First());
            endPointsMock.Setup(ep => ep.End).Returns(graph.Vertices.Last());

            var parentVertices = SetParentVertices(graph);
            var graphPath = new GraphPath(parentVertices, endPointsMock.Object);
            var path = graphPath.Path;

            Assert.IsTrue(path.Any());
        }

        private IDictionary<ICoordinate, IVertex> SetParentVertices(IGraph graph)
        {
            var dictionary = new Dictionary<ICoordinate, IVertex>();
            for (int i = 1; i < VerticesCount; i++)
            {
                dictionary[graph.Vertices.ElementAt(i).Position] = graph.Vertices.ElementAt(i - 1);
            }
            return dictionary;
        }
    }
}