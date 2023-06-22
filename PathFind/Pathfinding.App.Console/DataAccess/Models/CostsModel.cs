using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class CostsModel : IIdentityItem<long>
    {
        public long Id { get; set; }

        public long AlgorithmId { get; set; }

        public int[] Costs { get; set; }
    }
}
