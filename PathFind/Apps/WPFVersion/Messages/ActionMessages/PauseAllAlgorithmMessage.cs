using Commands.Interfaces;
using WPFVersion.ViewModel;

namespace WPFVersion.Messages
{
    internal sealed class PauseAllAlgorithmMessage : IExecutable<AlgorithmViewModel>
    {
        public void Execute(AlgorithmViewModel model)
        {
            model.Pause();
        }
    }
}
