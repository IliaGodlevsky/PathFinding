using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class VertexSerializationDto
    {
        public ICoordinate Position { get; set; }

        public IVertexCost Cost { get; set; }

        public bool IsObstacle { get; set; }

        public IReadOnlyCollection<ICoordinate> Neighbors { get; set; }
    }
}
