using Autofac.Features.Metadata;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class HeuristicsViewModel
    {
        public IReadOnlyDictionary<string, IHeuristic> Heuristics { get; }

        public HeuristicsViewModel(IEnumerable<(string Name, IHeuristic Heuristic)> heuristics)
        {
            Heuristics = heuristics
                .ToDictionary(x => x.Name, x => x.Heuristic)
                .AsReadOnly();
        }

        
        public HeuristicsViewModel(IEnumerable<Meta<IHeuristic>> heuristics)
            : this(heuristics.Select(x => ((string)x.Metadata[MetadataKeys.NameKey], x.Value)))
        {

        } 
    }
}
