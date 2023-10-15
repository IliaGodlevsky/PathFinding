using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Primitives.Single;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Tests.Realizations.GraphPaths
{
    internal sealed class DijkstraExpectedPath : Singleton<DijkstraExpectedPath, IGraphPath>, IGraphPath
    {
        public double Cost => 137;

        public int Count => 55;

        private DijkstraExpectedPath()
        {

        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            yield return new Coordinate(3, 5);
            yield return new Coordinate(3, 6);
            yield return new Coordinate(3, 7);
            yield return new Coordinate(4, 8);
            yield return new Coordinate(5, 9);
            yield return new Coordinate(6, 10);
            yield return new Coordinate(5, 11);
            yield return new Coordinate(4, 11);
            yield return new Coordinate(3, 12);
            yield return new Coordinate(3, 13);
            yield return new Coordinate(4, 14);
            yield return new Coordinate(5, 13);
            yield return new Coordinate(6, 13);
            yield return new Coordinate(7, 14);
            yield return new Coordinate(8, 15);
            yield return new Coordinate(9, 16);
            yield return new Coordinate(8, 17);
            yield return new Coordinate(9, 16);
            yield return new Coordinate(10, 17);
            yield return new Coordinate(11, 17);
            yield return new Coordinate(12, 16);
            yield return new Coordinate(13, 15);
            yield return new Coordinate(12, 14);
            yield return new Coordinate(11, 13);
            yield return new Coordinate(10, 13);
            yield return new Coordinate(9, 12);
            yield return new Coordinate(10, 11);
            yield return new Coordinate(10, 10);
            yield return new Coordinate(9, 9);
            yield return new Coordinate(10, 8);
            yield return new Coordinate(11, 7);
            yield return new Coordinate(12, 6);
            yield return new Coordinate(13, 7);
            yield return new Coordinate(14, 6);
            yield return new Coordinate(15, 5);
            yield return new Coordinate(14, 4);
            yield return new Coordinate(15, 3);
            yield return new Coordinate(16, 2);
            yield return new Coordinate(17, 3);
            yield return new Coordinate(18, 2);
            yield return new Coordinate(19, 3);
            yield return new Coordinate(20, 3);
            yield return new Coordinate(21, 4);
            yield return new Coordinate(22, 5);
            yield return new Coordinate(23, 6);
            yield return new Coordinate(22, 7);
            yield return new Coordinate(21, 8);
            yield return new Coordinate(22, 9);
            yield return new Coordinate(21, 10);
            yield return new Coordinate(22, 11);
            yield return new Coordinate(23, 12);
            yield return new Coordinate(22, 13);
            yield return new Coordinate(21, 14);
            yield return new Coordinate(21, 15);
            yield return new Coordinate(20, 16);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
