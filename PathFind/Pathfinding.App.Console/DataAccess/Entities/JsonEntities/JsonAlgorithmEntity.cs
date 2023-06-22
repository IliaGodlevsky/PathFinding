using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DataAccess.Entities.JsonEntities
{
    public class JsonAlgorithmEntity : IIdentityItem<long>
    {
        public long Id { get; set; }

        public long GraphId { get; set; }

        public string Name { get; set; }
    }
}
