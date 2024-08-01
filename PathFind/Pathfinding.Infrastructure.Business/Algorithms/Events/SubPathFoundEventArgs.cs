using Pathfinding.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Events
{
    public class SubPathFoundEventArgs : EventArgs
    {
        public IReadOnlyCollection<ICoordinate> SubPath { get; }

        public SubPathFoundEventArgs(IReadOnlyCollection<ICoordinate> subPath)
        {
            SubPath = subPath;
        }
    }
}
