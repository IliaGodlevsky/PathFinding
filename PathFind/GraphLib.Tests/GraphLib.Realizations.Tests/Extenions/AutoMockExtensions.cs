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
        public static void MockVertexCostFactory(this AutoMock self, Func<int, IVertexCost> returnCallback)
        {
            self.Mock<IVertexCostFactory>().Setup(x => x.CreateCost(It.IsAny<int>())).Returns<int>(returnCallback);
        }

        public static void MockGraphFactory<TGraph, TVertex>(this AutoMock self, Func<IReadOnlyCollection<TVertex>, int[], TGraph> returnCallback)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            self.Mock<IGraphFactory<TGraph, TVertex>>()
                .Setup(x => x.CreateGraph(It.IsAny<IReadOnlyCollection<TVertex>>(), It.IsAny<int[]>()))
                .Returns<IReadOnlyCollection<TVertex>, int[]>(returnCallback);
        }

        public static void MockCoordinateFactory(this AutoMock self, Func<int[], ICoordinate> returnCallback)
        {
            self.Mock<ICoordinateFactory>().Setup(x => x.CreateCoordinate(It.IsAny<int[]>())).Returns<int[]>(returnCallback);
        }

        public static void MockNeighbourhoodFactory(this AutoMock self, Func<ICoordinate, INeighborhood> returnCallback)
        {
            self.Mock<INeighborhoodFactory>().Setup(x => x.CreateNeighborhood(It.IsAny<ICoordinate>())).Returns<ICoordinate>(returnCallback);
        }

        public static void MockVertexFactory<TVertex>(this AutoMock self, Func<ICoordinate, TVertex> returnCallback)
            where TVertex : IVertex
        {
            self.Mock<IVertexFactory<TVertex>>()
                .Setup(x => x.CreateVertex(It.IsAny<ICoordinate>()))
                .Returns<ICoordinate>(returnCallback);
        }
    }
}
