using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Events
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
