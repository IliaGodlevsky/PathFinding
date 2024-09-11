using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel.RightPanelViewModels.RunViewModels
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
    }
}
