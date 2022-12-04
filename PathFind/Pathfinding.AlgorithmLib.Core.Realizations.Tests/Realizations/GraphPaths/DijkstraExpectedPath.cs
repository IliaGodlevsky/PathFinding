using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
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
            yield return new TestCoordinate(3, 5);
            yield return new TestCoordinate(3, 6);
            yield return new TestCoordinate(3, 7);
            yield return new TestCoordinate(4, 8);
            yield return new TestCoordinate(5, 9);
            yield return new TestCoordinate(6, 10);
            yield return new TestCoordinate(5, 11);
            yield return new TestCoordinate(4, 11);
            yield return new TestCoordinate(3, 12);
            yield return new TestCoordinate(3, 13);
            yield return new TestCoordinate(4, 14);
            yield return new TestCoordinate(5, 13);
            yield return new TestCoordinate(6, 13);
            yield return new TestCoordinate(7, 14);
            yield return new TestCoordinate(8, 15);
            yield return new TestCoordinate(9, 16);
            yield return new TestCoordinate(8, 17);
            yield return new TestCoordinate(9, 16);
            yield return new TestCoordinate(10, 17);
            yield return new TestCoordinate(11, 17);
            yield return new TestCoordinate(12, 16);
            yield return new TestCoordinate(13, 15);
            yield return new TestCoordinate(12, 14);
            yield return new TestCoordinate(11, 13);
            yield return new TestCoordinate(10, 13);
            yield return new TestCoordinate(9, 12);
            yield return new TestCoordinate(10, 11);
            yield return new TestCoordinate(10, 10);
            yield return new TestCoordinate(9, 9);
            yield return new TestCoordinate(10, 8);
            yield return new TestCoordinate(11, 7);
            yield return new TestCoordinate(12, 6);
            yield return new TestCoordinate(13, 7);
            yield return new TestCoordinate(14, 6);
            yield return new TestCoordinate(15, 5);
            yield return new TestCoordinate(14, 4);
            yield return new TestCoordinate(15, 3);
            yield return new TestCoordinate(16, 2);
            yield return new TestCoordinate(17, 3);
            yield return new TestCoordinate(18, 2);
            yield return new TestCoordinate(19, 3);
            yield return new TestCoordinate(20, 3);
            yield return new TestCoordinate(21, 4);
            yield return new TestCoordinate(22, 5);
            yield return new TestCoordinate(23, 6);
            yield return new TestCoordinate(22, 7);
            yield return new TestCoordinate(21, 8);
            yield return new TestCoordinate(22, 9);
            yield return new TestCoordinate(21, 10);
            yield return new TestCoordinate(22, 11);
            yield return new TestCoordinate(23, 12);
            yield return new TestCoordinate(22, 13);
            yield return new TestCoordinate(21, 14);
            yield return new TestCoordinate(21, 15);
            yield return new TestCoordinate(20, 16);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
