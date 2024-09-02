using ReactiveUI;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class RunModel : ReactiveObject
    {
        public int RunId { get; set; }

        public string Algorithm { get; set; }
    }
}
