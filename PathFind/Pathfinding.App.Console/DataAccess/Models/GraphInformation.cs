using System;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class GraphInformationModel : IIdentityItem<Guid>
    {
        public Guid Id { get; set; }

        public Guid GraphId { get; set; }

        public string Description { get; set; }
    }
}
