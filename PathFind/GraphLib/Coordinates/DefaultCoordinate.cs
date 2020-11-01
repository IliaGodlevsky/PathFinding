using GraphLib.Coordinates.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Coordinates
{
    [Serializable]
    public sealed class DefaultCoordinate : ICoordinate
    {
        public IEnumerable<int> Coordinates => new int[] { };

        public IEnumerable<ICoordinate> Environment => new DefaultCoordinate[] { };

        public bool IsDefault => true;

        public DefaultCoordinate()
        {

        }

        public object Clone()
        {
            return new DefaultCoordinate();
        }
    }
}
