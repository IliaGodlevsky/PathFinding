using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class CostsModel : IIdentityItem<Guid>
    {
        public Guid Id { get; set; }

        public Guid AlgorithmId { get; set; }

        public int[] Costs { get; set; }
    }
}
