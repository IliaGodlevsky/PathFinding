namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class RunSelectedMessage
    {
        public int[] SelectedRuns { get; }

        public RunSelectedMessage(int[] selectedRuns)
        {
            SelectedRuns = selectedRuns;
        }
    }
}
