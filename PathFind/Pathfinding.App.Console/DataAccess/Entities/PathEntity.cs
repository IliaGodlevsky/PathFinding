using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal sealed class PathEntity : IEntity<int>
    {
        public int Id { get; set; }

        public Guid AlgorithmId { get; set; }

        public IList<ICoordinate> Path { get; set; }
    }
}
