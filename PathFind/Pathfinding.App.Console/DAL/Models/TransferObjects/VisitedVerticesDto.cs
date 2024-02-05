using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class VisitedVerticesDto
    {
        public CoordinateDto Current { get; set; }

        public IReadOnlyCollection<CoordinateDto> Enqueued { get; set; }
    }
}
