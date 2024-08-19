using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Business.Test.Mock;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Shared.Primitives;
using System.Collections;

namespace Pathfinding.Infrastructure.Business.Test.Algorithms.DataProviders
{
    internal static class WaveAlgorithmDataProvider
    {
        public static IEnumerable DijkstraDataProvider
        {
            get
            {
                var graph = TestGraph.Instance;

                return new TestCaseData[]
                {
                    new TestCaseData(new List<TestVertex>()
                    {
                        graph.Get(0, 0),
                        graph.Get(5, 5),
                        graph.Get(14, 17),
                        graph.Get(18, 25),
                        graph.Get(25, 18),
                        graph.Get(29, 34)
                    },
                    new List<Coordinate>()
                    {
                        new Coordinate(1, 0),
                        new Coordinate(2, 1),
                        new Coordinate(3, 2),
                        new Coordinate(3, 3),
                        new Coordinate(4, 4),
                        new Coordinate(5, 5),
                        new Coordinate(5, 6),
                        new Coordinate(6, 7),
                        new Coordinate(7, 7),
                        new Coordinate(8, 8),
                        new Coordinate(9, 9),
                        new Coordinate(10, 10),
                        new Coordinate(9, 11),
                        new Coordinate(8, 12),
                        new Coordinate(9, 13),
                        new Coordinate(10, 14),
                        new Coordinate(11, 15),
                        new Coordinate(12, 15),
                        new Coordinate(13, 16),
                        new Coordinate(14, 17),
                        new Coordinate(14, 18),
                        new Coordinate(13, 19),
                        new Coordinate(12, 20),
                        new Coordinate(13, 21),
                        new Coordinate(13, 22),
                        new Coordinate(14, 23),
                        new Coordinate(15, 24),
                        new Coordinate(16, 25),
                        new Coordinate(17, 25),
                        new Coordinate(18, 25),
                        new Coordinate(17, 24),
                        new Coordinate(17, 23),
                        new Coordinate(18, 22),
                        new Coordinate(18, 21),
                        new Coordinate(19, 20),
                        new Coordinate(20, 19),
                        new Coordinate(21, 18),
                        new Coordinate(22, 17),
                        new Coordinate(23, 16),
                        new Coordinate(24, 17),
                        new Coordinate(25, 18),
                        new Coordinate(24, 19),
                        new Coordinate(24, 20),
                        new Coordinate(23, 21),
                        new Coordinate(22, 22),
                        new Coordinate(22, 23),
                        new Coordinate(23, 24),
                        new Coordinate(23, 25),
                        new Coordinate(24, 26),
                        new Coordinate(25, 27),
                        new Coordinate(26, 28),
                        new Coordinate(27, 29),
                        new Coordinate(27, 30),
                        new Coordinate(28, 31),
                        new Coordinate(29, 32),
                        new Coordinate(29, 33),
                        new Coordinate(29, 34)
                    })
                };
            }
        }
    }
}
