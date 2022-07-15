using Commands.Interfaces;
using WPFVersion.ViewModel;

namespace WPFVersion.Messages
{
    internal sealed class InterruptAllAlgorithmsMessage : IExecutable<AlgorithmViewModel>
    {
        public void Execute(AlgorithmViewModel model)
        {
            model.Interrupt();
        }
    }
}
