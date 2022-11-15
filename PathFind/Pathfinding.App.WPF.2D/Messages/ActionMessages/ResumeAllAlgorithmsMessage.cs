using Pathfinding.App.WPF._2D.ViewModel;
using Shared.Executable;

namespace Pathfinding.App.WPF._2D.Messages.ActionMessages
{
    internal sealed class ResumeAllAlgorithmsMessage : IExecutable<AlgorithmViewModel>
    {
        public void Execute(AlgorithmViewModel model)
        {
            model.Resume();
        }
    }
}
