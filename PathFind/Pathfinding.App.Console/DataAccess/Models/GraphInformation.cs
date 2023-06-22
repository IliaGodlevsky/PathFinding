using System;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class GraphInformationModel : IIdentityItem<long>
    {
        public long Id { get; set; }

        public long GraphId { get; set; }

        public string Description { get; set; }
    }
}
