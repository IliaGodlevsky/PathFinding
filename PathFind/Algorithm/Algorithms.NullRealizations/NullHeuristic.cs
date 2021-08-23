﻿using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NullObject.Attributes;

namespace Algorithm.NullRealizations
{
    /// <summary>
    /// Represents a null analog for <see cref="IHeuristic"/>
    /// interface. This class can't be inherited
    /// </summary>
    [Null]
    public sealed class NullHeuristic : IHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            return default;
        }
    }
}
