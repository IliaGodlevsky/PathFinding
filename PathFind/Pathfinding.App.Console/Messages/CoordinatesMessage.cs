using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Messages
{
    internal abstract class CoordinatesMessage
    {
        public IReadOnlyCollection<ICoordinate> Coordinates { get; set; }
    }
}
