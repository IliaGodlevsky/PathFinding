﻿using GraphLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates.Abstractions
{
    [Serializable]
    public abstract class BaseCoordinate : ICoordinate
    {
        public BaseCoordinate(params int[] coordinates)
        {
            Coordinates = coordinates;
        }

        public IEnumerable<int> Coordinates { get; }

        public bool IsDefault => false;

        public override bool Equals(object pos)
        {
            if (pos is ICoordinate coordinate)
            {
                return coordinate.IsEqual(this);
            }

            throw new ArgumentException("Invalid value to compare");
        }

        public override int GetHashCode()
        {
            return Coordinates.Aggregate((x, y) => x ^ y);
        }

        public abstract IEnumerable<ICoordinate> Environment { get; }

        public abstract object Clone();
    }
}