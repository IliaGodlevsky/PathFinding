using Common.Interface;
using GraphLib.Base;
using GraphLib.Interfaces;
using System;
using System.Diagnostics;

namespace GraphLib.Realizations.Coordinates
{
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    internal sealed class CoordinateProxy : BaseCoordinate, ICoordinate, ICloneable<ICoordinate>
    {

        public CoordinateProxy(int[] coordinates) 
            : base(coordinates.Length, coordinates)
        {

        }

        public CoordinateProxy(ICoordinate coordinate)
            : this(coordinate.CoordinatesValues)
        {

        }

        public override ICoordinate Clone()
        {
            return new CoordinateProxy(this);
        }
    }
}