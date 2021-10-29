using GraphLib.Interfaces;
using NullObject.Attributes;
using System;

namespace GraphLib.NullRealizations.NullObjects
{
    /// <summary>
    /// An empty coordinate with default realization
    /// </summary>
    [Null]
    [Serializable]
    public sealed class NullCoordinate : ICoordinate
    {
        public static ICoordinate Instance => instance.Value;

        public int[] CoordinatesValues => new int[] { };

        public ICoordinate Clone()
        {
            return new NullCoordinate();
        }

        public override bool Equals(object pos)
        {
            return pos is NullCoordinate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private NullCoordinate()
        {

        }

        private static readonly Lazy<ICoordinate> instance = new Lazy<ICoordinate>(() => new NullCoordinate());
    }
}
