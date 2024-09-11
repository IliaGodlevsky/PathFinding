using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels
{
    internal sealed class SmoothLevelViewModel : ReactiveObject
    {
        public IReadOnlyDictionary<string, int> Levels { get; set; }

        public SmoothLevelViewModel(IEnumerable<(string Name, int Level)> levels)
        {
            Levels = levels.ToDictionary(x => x.Name, x => x.Level);
        }
    }
}
