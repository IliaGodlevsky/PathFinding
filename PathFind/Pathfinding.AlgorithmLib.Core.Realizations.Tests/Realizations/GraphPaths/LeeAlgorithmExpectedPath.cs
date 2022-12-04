using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
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
            yield return new TestCoordinate(3, 5);
            yield return new TestCoordinate(3, 6);
            yield return new TestCoordinate(3, 7);
            yield return new TestCoordinate(2, 8);
            yield return new TestCoordinate(2, 9);
            yield return new TestCoordinate(1, 10);
            yield return new TestCoordinate(1, 11);
            yield return new TestCoordinate(2, 12);
            yield return new TestCoordinate(3, 13);
            yield return new TestCoordinate(4, 13);
            yield return new TestCoordinate(5, 14);
            yield return new TestCoordinate(6, 15);
            yield return new TestCoordinate(7, 16);
            yield return new TestCoordinate(8, 17);
            yield return new TestCoordinate(7, 16);
            yield return new TestCoordinate(8, 15);
            yield return new TestCoordinate(9, 14);
            yield return new TestCoordinate(10, 13);
            yield return new TestCoordinate(11, 13);
            yield return new TestCoordinate(10, 12);
            yield return new TestCoordinate(9, 11);
            yield return new TestCoordinate(8, 10);
            yield return new TestCoordinate(9, 9);
            yield return new TestCoordinate(10, 8);
            yield return new TestCoordinate(11, 7);
            yield return new TestCoordinate(12, 6);
            yield return new TestCoordinate(12, 5);
            yield return new TestCoordinate(13, 4);
            yield return new TestCoordinate(14, 3);
            yield return new TestCoordinate(15, 2);
            yield return new TestCoordinate(16, 2);
            yield return new TestCoordinate(17, 1);
            yield return new TestCoordinate(18, 2);
            yield return new TestCoordinate(19, 3);
            yield return new TestCoordinate(20, 3);
            yield return new TestCoordinate(21, 4);
            yield return new TestCoordinate(20, 5);
            yield return new TestCoordinate(19, 6);
            yield return new TestCoordinate(20, 7);
            yield return new TestCoordinate(21, 8);
            yield return new TestCoordinate(22, 9);
            yield return new TestCoordinate(21, 10);
            yield return new TestCoordinate(22, 11);
            yield return new TestCoordinate(23, 12);
            yield return new TestCoordinate(22, 13);
            yield return new TestCoordinate(21, 14);
            yield return new TestCoordinate(20, 15);
            yield return new TestCoordinate(20, 16);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
