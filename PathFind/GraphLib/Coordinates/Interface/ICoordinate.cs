using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace GraphLib.Coordinates.Interface
{
    public interface ICoordinate : ICloneable, IDefault
    {
        IEnumerable<int> Coordinates { get; }
        IEnumerable<ICoordinate> Environment { get; }
    }
}
