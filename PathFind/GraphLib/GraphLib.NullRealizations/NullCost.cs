using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System.Diagnostics;

namespace GraphLib.NullRealizations
{
    [Null]
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
