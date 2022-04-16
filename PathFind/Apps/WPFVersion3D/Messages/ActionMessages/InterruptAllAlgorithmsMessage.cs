using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.ActionMessages
{
    internal sealed class InterruptAllAlgorithmsMessage : BaseAlgorithmsExecutionMessage
    {
        protected override void Execute(AlgorithmViewModel model)
        {
            model.Interrupt();
        }
    }
}
