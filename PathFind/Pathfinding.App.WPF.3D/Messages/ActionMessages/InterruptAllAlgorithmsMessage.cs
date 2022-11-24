using Pathfinding.App.WPF._3D.ViewModel;
using Shared.Executable;

namespace Pathfinding.App.WPF._3D.Messages.ActionMessages
{
    internal sealed class InterruptAllAlgorithmsMessage : IExecutable<AlgorithmViewModel>
    {
        public void Execute(AlgorithmViewModel model)
        {
            model.Interrupt();
        }
    }
}
