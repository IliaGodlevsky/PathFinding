using WPFVersion3D.Messages.ActionMessages;
using WPFVersion3D.ViewModel.BaseViewModel;

namespace WPFVersion3D.ViewModel.ButtonViewModels
{
    internal class InterruptAllAlgorithmsViewModel : BaseAllAlgorithmsOperationViewModel
    {
        protected override void ExecuteCommand(object param)
        {
            messenger.Send(new InterruptAllAlgorithmsMessage());
        }
    }
}
