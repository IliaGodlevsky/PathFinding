using GraphLib.Base;
using System;

namespace GraphLib.Serialization.Tests.Objects
{
    [Serializable]
    internal class TestCost : BaseVertexCost
    {
        public TestCost(int cost = 1) : base(cost)
        {

        }

        public TestCost() : base()
        {

        }
    }
}
