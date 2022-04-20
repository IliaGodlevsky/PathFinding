using WPFVersion.Messages;
using WPFVersion.ViewModel.BaseViewModels;

namespace WPFVersion.ViewModel.ButtonViewModels
{
    internal class InterruptAllAlgorithmsViewModel : BaseAllAlgorithmsOperationViewModel
    {
        protected override void ExecuteCommand(object param)
        {
            messenger.Send(new InterruptAllAlgorithmsMessage());
        }
    }
}
