using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.ActionMessages
{
    internal sealed class PauseAllAlgorithmMessage : BaseAlgorithmsExecutionMessage
    {
        protected override void Execute(AlgorithmViewModel model)
        {
            model.Pause();
        }
    }
}
