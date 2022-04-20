using WPFVersion.Messages;
using WPFVersion.ViewModel.BaseViewModels;

namespace WPFVersion.ViewModel.ButtonViewModels
{
    internal class PauseAllAlgorithmsViewModel : BaseAllAlgorithmsOperationViewModel
    {
        protected override void ExecuteCommand(object param)
        {
            messenger.Send(new PauseAllAlgorithmMessage());
        }
    }
}
