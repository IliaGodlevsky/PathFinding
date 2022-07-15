using Commands.Interfaces;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.ActionMessages
{
    internal sealed class InterruptAllAlgorithmsMessage : IExecutable<AlgorithmViewModel>
    {
        public void Execute(AlgorithmViewModel model)
        {
            model.Interrupt();
        }
    }
}
