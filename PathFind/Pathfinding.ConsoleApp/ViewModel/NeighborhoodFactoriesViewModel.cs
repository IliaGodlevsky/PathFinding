using Autofac.Features.Metadata;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class NeighborhoodFactoriesViewModel
    {
        public IReadOnlyDictionary<string, INeighborhoodFactory> Factories { get; }

        public NeighborhoodFactoriesViewModel(IEnumerable<(string Name, INeighborhoodFactory Factory)> factories)
        {
            Factories = factories.ToDictionary(x => x.Name, x => x.Factory);
        }

        public NeighborhoodFactoriesViewModel(IEnumerable<Meta<INeighborhoodFactory>> factories)
        {
            Factories = factories.ToDictionary(x => (string)x.Metadata[MetadataKeys.NameKey], x => x.Value);
        }
    }
}
