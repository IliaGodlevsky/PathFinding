using Pathfinding.App.Console.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class CostsInMemoryStorage : InMemoryStorage<CostsEntity, int>
    {
        private static int id = 0;

        protected override int NextId => id++;
    }
}
