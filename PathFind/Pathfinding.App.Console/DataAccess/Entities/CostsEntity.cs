using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class CostsEntity : IEntity<int>
    {
        public int Id { get; set; }

        public Guid AlgorithmId { get; set; }

        public IList<int> Costs { get; set; }
    }
}
