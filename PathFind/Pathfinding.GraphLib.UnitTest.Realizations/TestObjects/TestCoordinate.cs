using Pathfinding.GraphLib.Core.Abstractions;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestObjects
{
    public sealed class TestCoordinate : Coordinate
    {
        public TestCoordinate(IReadOnlyList<int> coordinates)
            : base(coordinates.Count, coordinates)
        {

        }

        public TestCoordinate(params int[] coordinates)
            : this((IReadOnlyList<int>)coordinates)
        {

        }
    }
}