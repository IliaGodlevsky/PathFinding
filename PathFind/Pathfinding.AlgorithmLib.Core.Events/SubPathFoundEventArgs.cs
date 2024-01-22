using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Events
{
    public class SubPathFoundEventArgs : EventArgs
    {
        public IEnumerable<ICoordinate> SubPath { get; }

        public SubPathFoundEventArgs(IEnumerable<ICoordinate> subPath)
        {
            SubPath = subPath;
        }
    }
}
