using GraphLib.Interface;
using System;

namespace GraphLib.Tests.TestInfrastructure.Objects
{
    [Serializable]
    internal class TestCost : IVertexCost
    {
        public TestCost(int cost = 1)
        {
            CurrentCost = cost;
        }

        public override bool Equals(object obj)
        {
            if (obj is IVertexCost cost)
            {
                return cost.CurrentCost == CurrentCost;
            }
            throw new Exception();
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
        }

        public int CurrentCost { get; }

        public object Clone()
        {
            return new TestCost(CurrentCost);
        }
    }
}
