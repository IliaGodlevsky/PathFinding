using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class CoordinatesModel : IIdentityItem<Guid>
    {
        public Guid Id { get; set; }

        public Guid AlgorithmId { get; set; }

        public IReadOnlyList<ICoordinate> Coordinates { get; set; }
    }
}
