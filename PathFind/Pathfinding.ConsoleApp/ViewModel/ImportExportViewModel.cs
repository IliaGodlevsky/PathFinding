using CommunityToolkit.Mvvm.Messaging;
using ReactiveUI;
using System.Reactive;
using System.Windows.Forms;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal abstract class ImportExportViewModel : BaseViewModel
    {
        private bool withRange;
        public bool WithRange 
        {
            get => withRange;
            set => this.RaiseAndSetIfChanged(ref withRange, value);
        }

        private bool withRuns;
        public bool WithRuns
        {
            get => withRuns;
            set => this.RaiseAndSetIfChanged(ref withRuns, value);
        }
    }
}
