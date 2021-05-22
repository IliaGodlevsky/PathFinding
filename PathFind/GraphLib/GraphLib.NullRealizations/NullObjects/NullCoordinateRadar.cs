using GraphLib.Interfaces;
using NullObject.Attributes;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullCoordinateRadar : ICoordinateRadar
    {
        public NullCoordinateRadar()
        {
            environment = new NullCoordinate[] { new NullCoordinate() };
        }

        public IEnumerable<ICoordinate> Environment => environment;

        private readonly NullCoordinate[] environment;
    }
}