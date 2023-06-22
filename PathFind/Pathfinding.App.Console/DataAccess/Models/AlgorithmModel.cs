using System;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class AlgorithmModel : IIdentityItem<long>
    {
        public long Id { get; set; }

        public long GraphId { get; set; }

        public string Name { get; set; }
    }
}
