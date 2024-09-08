using ReactiveUI;
using System.Reactive;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal abstract class CreateRunViewModel : BaseViewModel
    {
        public abstract ReactiveCommand<MouseEventArgs, Unit> CreateRunCommand { get; }
    }
}
