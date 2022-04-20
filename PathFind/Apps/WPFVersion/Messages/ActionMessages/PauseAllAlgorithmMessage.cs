using WPFVersion.Messages.BaseMessages;
using WPFVersion.ViewModel;

namespace WPFVersion.Messages
{
    internal sealed class PauseAllAlgorithmMessage : BaseAlgorithmsExecutionMessage
    {
        protected override void Execute(AlgorithmViewModel model)
        {
            model.Pause();
        }
    }
}
