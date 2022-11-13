using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathfindingAlgorithmChosen
    {
        public IAlgorithmFactory<PathfindingProcess> Algorithm { get; }

        public PathfindingAlgorithmChosen(IAlgorithmFactory<PathfindingProcess> algorithm)
        {
            Algorithm = algorithm;
        }
    }
}
