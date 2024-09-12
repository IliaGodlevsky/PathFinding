using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class NeighborhoodFactoryViewModel : ReactiveObject
    {
        public IReadOnlyDictionary<string, INeighborhoodFactory> Factories { get; }

        public NeighborhoodFactoryViewModel(IEnumerable<(string Name, INeighborhoodFactory Factory)> factories)
        {
            Factories = factories.ToDictionary(x => x.Name, x => x.Factory);
        }
    }
}
