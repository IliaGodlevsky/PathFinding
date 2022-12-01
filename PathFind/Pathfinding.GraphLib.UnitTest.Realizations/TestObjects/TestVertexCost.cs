using Pathfinding.GraphLib.Core.Realizations;
using System;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestObjects
{
    public sealed class TestVertexCost : VertexCost
    {
        public TestVertexCost(int cost)
            : base(cost, default)
        {

        }
    }
}
