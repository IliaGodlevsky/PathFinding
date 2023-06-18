using System;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class AlgorithmModel : IIdentityItem<Guid>
    {
        public Guid Id { get; set; }

        public Guid GraphId { get; set; }

        public string Name { get; set; }
    }
}
