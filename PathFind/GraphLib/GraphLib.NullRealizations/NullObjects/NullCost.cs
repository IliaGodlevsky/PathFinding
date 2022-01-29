using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;
using System.Diagnostics;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    [DebuggerDisplay("Null")]
    public sealed class NullCost : Singleton<NullCost>, IVertexCost
    {
        public int CurrentCost => default;

        public override bool Equals(object obj)
        {
            return obj is NullCost;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IVertexCost Clone()
        {
            return Instance;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        private NullCost()
        {

        }
    }
}
