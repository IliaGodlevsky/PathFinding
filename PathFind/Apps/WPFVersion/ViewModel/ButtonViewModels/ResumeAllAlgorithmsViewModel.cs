using WPFVersion.Messages;
using WPFVersion.ViewModel.BaseViewModels;

namespace WPFVersion.ViewModel.ButtonViewModels
{
    internal class ResumeAllAlgorithmViewModel : BaseAllAlgorithmsOperationViewModel
    {
        protected override void ExecuteCommand(object param)
        {
            messenger.Send(new ResumeAllAlgorithmsMessage());
        }
    }
}
