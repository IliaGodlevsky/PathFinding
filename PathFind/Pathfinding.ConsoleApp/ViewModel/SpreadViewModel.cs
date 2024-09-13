using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class SpreadViewModel
    {
        public IReadOnlyDictionary<string, int> SpreadLevels { get; }

        public SpreadViewModel([KeyFilter(KeyFilters.SpreadLevels)] IEnumerable<(string Name, int Spread)> spreadLevels)
        {
            SpreadLevels = spreadLevels.ToDictionary(x => x.Name, x => x.Spread).AsReadOnly();
        }
    }
}
