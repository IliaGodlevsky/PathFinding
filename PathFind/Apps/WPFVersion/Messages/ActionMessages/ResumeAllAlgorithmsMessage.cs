using Commands.Interfaces;
using WPFVersion.ViewModel;

namespace WPFVersion.Messages
{
    internal sealed class ResumeAllAlgorithmsMessage : IExecutable<AlgorithmViewModel>
    {
        public void Execute(AlgorithmViewModel model)
        {
            model.Resume();
        }
    }
}
