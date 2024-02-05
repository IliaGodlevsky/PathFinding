using Pathfinding.AlgorithmLib.Core.Interface;
using System;

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
