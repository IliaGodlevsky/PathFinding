using Autofac.Extras.Moq;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Moq;
using System;
using System.Collections.Generic;

namespace GraphLib.Realizations.Tests.Extenions
{
    internal static class AutoMockExtensions
    {
        public static void MockCoordinate(this AutoMock self, int[] coordinateValues)
        {
            self.Mock<ICoordinate>().Setup(c => c.CoordinatesValues).Returns(coordinateValues);
        }

        public static void MockVertexCostFactory(this AutoMock self, Func<int, IVertexCost> returnCallback)
        {
            self.Mock<IVertexCostFactory>().Setup(x => x.CreateCost(It.IsAny<int>())).Returns<int>(returnCallback);
        }

        public static void MockGraphFactory(this AutoMock self, Func<IReadOnlyCollection<IVertex>, int[], IGraph> returnCallback)
        {
            self.Mock<IGraphFactory>()
                .Setup(x => x.CreateGraph(It.IsAny<IReadOnlyCollection<IVertex>>(), It.IsAny<int[]>()))
                .Returns<IReadOnlyCollection<IVertex>, int[]>(returnCallback);
        }

        public static void MockCoordinateFactory(this AutoMock self, Func<int[], ICoordinate> returnCallback)
        {
            self.Mock<ICoordinateFactory>().Setup(x => x.CreateCoordinate(It.IsAny<int[]>())).Returns<int[]>(returnCallback);
        }

        public static void MockNeighbourhoodFactory(this AutoMock self, Func<ICoordinate, INeighborhood> returnCallback)
        {
            self.Mock<INeighborhoodFactory>().Setup(x => x.CreateNeighborhood(It.IsAny<ICoordinate>())).Returns<ICoordinate>(returnCallback);
        }

        public static void MockVertexFactory(this AutoMock self, Func<INeighborhood, ICoordinate, IVertex> returnCallback)
        {
            self.Mock<IVertexFactory>()
                .Setup(x => x.CreateVertex(It.IsAny<INeighborhood>(), It.IsAny<ICoordinate>()))
                .Returns<INeighborhood, ICoordinate>(returnCallback);
        }
    }
}
