using ReactiveUI;

namespace Pathfinding.ConsoleApp.ViewModel
{
    //TODO: Implement logic for chosing type of export
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
