using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Dto
{
    internal class VerticesDto : IDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ReferenceId { get; set; }

        public IList<ICoordinate> Coordinates { get; set; }
    }
}
