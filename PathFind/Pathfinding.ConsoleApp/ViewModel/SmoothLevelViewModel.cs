using Autofac.Features.AttributeFilters;
using Autofac.Features.Metadata;
using Pathfinding.ConsoleApp.Injection;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class SmoothLevelViewModel : ReactiveObject
    {
        public IReadOnlyDictionary<string, int> Levels { get; set; }

        public SmoothLevelViewModel([KeyFilter(KeyFilters.SmoothLevels)] IEnumerable<(string Name, int Level)> levels)
        {
            Levels = levels.ToDictionary(x => x.Name, x => x.Level);
        }

        public SmoothLevelViewModel([KeyFilter(KeyFilters.SmoothLevels)] IEnumerable<Meta<string>> smoothLevels)
        {
            Levels = smoothLevels.ToDictionary(x => x.Value, x => (int)x.Metadata[MetadataKeys.SmoothKey]);
        }
    }
}
