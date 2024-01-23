using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Events
{
    public class SubPathFoundEventArgs : EventArgs
    {
        public IGraphPath SubPath { get; }

        public SubPathFoundEventArgs(IGraphPath subPath)
        {
            SubPath = subPath;
        }
    }
}
