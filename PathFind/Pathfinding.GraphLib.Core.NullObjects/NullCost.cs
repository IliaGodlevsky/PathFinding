using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Single;
using System.Diagnostics;

namespace Pathfinding.GraphLib.Core.NullObjects
{
    [DebuggerDisplay("Null")]
    public sealed class NullCost : Singleton<NullCost, IVertexCost>, IVertexCost
    {
        public int CurrentCost => default;

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
    }
}
