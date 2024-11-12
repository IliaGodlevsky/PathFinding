using Autofac.Features.Metadata;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class SmoothLevelsViewModel : ReactiveObject
    {
        public IReadOnlyDictionary<string, ILayer> Levels { get; set; }

        public SmoothLevelsViewModel(IEnumerable<(string Name, int Level, ILayer Layer)> levels)
        {
            Levels = levels.ToDictionary(x => x.Name, x => (ILayer)Enumerable.Repeat(x.Layer, x.Level).To(x => new Layers(x)));
        }

        public SmoothLevelsViewModel(IEnumerable<Meta<ILayer>> levels)
        {
            Levels = levels.ToDictionary(x => (string)x.Metadata[MetadataKeys.NameKey],
                x =>
                {
                    int metadata = (int)x.Metadata[MetadataKeys.SmoothKey];
                    var repeat = Enumerable.Repeat(x.Value, metadata).ToArray();
                    return repeat.To(x => (ILayer)new Layers(x));
                });
        }
    }
}
