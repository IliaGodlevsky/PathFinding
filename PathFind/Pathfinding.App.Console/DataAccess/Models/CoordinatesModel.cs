using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class CoordinatesModel : IIdentityItem<long>
    {
        public long Id { get; set; }

        public long AlgorithmId { get; set; }

        public IReadOnlyList<ICoordinate> Coordinates { get; set; }
    }
}
