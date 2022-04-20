using WPFVersion.Messages.BaseMessages;
using WPFVersion.ViewModel;

namespace WPFVersion.Messages
{
    internal sealed class ResumeAllAlgorithmsMessage : BaseAlgorithmsExecutionMessage
    {
        protected override void Execute(AlgorithmViewModel model)
        {
            model.Resume();
        }
    }
}
