using GraphLib.Coordinates.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Coordinates
{
    [Serializable]
    public sealed class NullCoordinate : ICoordinate
    {
        public static NullCoordinate Instance
        {
            get
            {
                if (instance == null)
                    instance = new NullCoordinate();
                return instance;
            }
        }

        public IEnumerable<int> Coordinates => new int[] { };

        public IEnumerable<ICoordinate> Environment => new NullCoordinate[] { };

        private NullCoordinate()
        {

        }

        private static NullCoordinate instance = null;

        public object Clone()
        {
            return instance;
        }
    }
}
