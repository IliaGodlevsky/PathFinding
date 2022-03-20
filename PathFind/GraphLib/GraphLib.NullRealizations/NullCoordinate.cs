using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;
using System.Diagnostics;

namespace GraphLib.NullRealizations
{
    [Null]
    [Serializable]
    [DebuggerDisplay("Null")]
    public sealed class NullCoordinate : Singleton<NullCoordinate, ICoordinate>, ICoordinate
    {
        public int[] CoordinatesValues => Array.Empty<int>();

        private NullCoordinate()
        {

        }

        public ICoordinate Clone()
        {
            return Instance;
        }

        public override bool Equals(object pos)
        {
            return pos is NullCoordinate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
