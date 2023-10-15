using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Primitives.Single;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Tests.Realizations.GraphPaths
{
    internal sealed class LeeAlgorithmExpectedPath : Singleton<LeeAlgorithmExpectedPath, IGraphPath>, IGraphPath
    {
        public double Cost => 208;

        public int Count => 48;

        private LeeAlgorithmExpectedPath()
        {

        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            yield return new Coordinate(3, 5);
            yield return new Coordinate(3, 6);
            yield return new Coordinate(3, 7);
            yield return new Coordinate(2, 8);
            yield return new Coordinate(2, 9);
            yield return new Coordinate(1, 10);
            yield return new Coordinate(1, 11);
            yield return new Coordinate(2, 12);
            yield return new Coordinate(3, 13);
            yield return new Coordinate(4, 13);
            yield return new Coordinate(5, 14);
            yield return new Coordinate(6, 15);
            yield return new Coordinate(7, 16);
            yield return new Coordinate(8, 17);
            yield return new Coordinate(7, 16);
            yield return new Coordinate(8, 15);
            yield return new Coordinate(9, 14);
            yield return new Coordinate(10, 13);
            yield return new Coordinate(11, 13);
            yield return new Coordinate(10, 12);
            yield return new Coordinate(9, 11);
            yield return new Coordinate(8, 10);
            yield return new Coordinate(9, 9);
            yield return new Coordinate(10, 8);
            yield return new Coordinate(11, 7);
            yield return new Coordinate(12, 6);
            yield return new Coordinate(12, 5);
            yield return new Coordinate(13, 4);
            yield return new Coordinate(14, 3);
            yield return new Coordinate(15, 2);
            yield return new Coordinate(16, 2);
            yield return new Coordinate(17, 1);
            yield return new Coordinate(18, 2);
            yield return new Coordinate(19, 3);
            yield return new Coordinate(20, 3);
            yield return new Coordinate(21, 4);
            yield return new Coordinate(20, 5);
            yield return new Coordinate(19, 6);
            yield return new Coordinate(20, 7);
            yield return new Coordinate(21, 8);
            yield return new Coordinate(22, 9);
            yield return new Coordinate(21, 10);
            yield return new Coordinate(22, 11);
            yield return new Coordinate(23, 12);
            yield return new Coordinate(22, 13);
            yield return new Coordinate(21, 14);
            yield return new Coordinate(20, 15);
            yield return new Coordinate(20, 16);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
