using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Events
{
    public class SubPathFoundEventArgs : EventArgs
    {
        public IReadOnlyCollection<Coordinate> SubPath { get; }

        public SubPathFoundEventArgs(IReadOnlyCollection<Coordinate> subPath)
        {
            SubPath = subPath;
        }
    }
}
