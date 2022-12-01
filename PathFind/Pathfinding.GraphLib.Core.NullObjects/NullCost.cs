using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Single;
using Shared.Primitives.ValueRange;
using System.Diagnostics;

namespace Pathfinding.GraphLib.Core.NullObjects
{
    [DebuggerDisplay("Null")]
    public sealed class NullCost : Singleton<NullCost, IVertexCost>, IVertexCost
    {
        public int CurrentCost => default;

        public InclusiveValueRange<int> CostRange => default;

        private NullCost()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is NullCost;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Empty;
        }

        public IVertexCost SetCost(int cost)
        {
            return Instance;
        }
    }
}
