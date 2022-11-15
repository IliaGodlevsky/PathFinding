using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.App.WPF._2D.ViewModel.BaseViewModels;

namespace Pathfinding.App.WPF._2D.ViewModel.ButtonViewModels
{
    internal class PauseAllAlgorithmsViewModel : BaseAllAlgorithmsOperationViewModel
    {
        protected override void ExecuteCommand(object param)
        {
            messenger.Send(new PauseAllAlgorithmMessage());
        }
    }
}
