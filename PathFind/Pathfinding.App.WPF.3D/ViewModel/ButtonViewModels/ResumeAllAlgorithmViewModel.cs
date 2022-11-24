using Pathfinding.App.WPF._3D.Messages.ActionMessages;
using Pathfinding.App.WPF._3D.ViewModel.BaseViewModel;

namespace Pathfinding.App.WPF._3D.ViewModel.ButtonViewModels
{
    internal class ResumeAllAlgorithmViewModel : BaseAllAlgorithmsOperationViewModel
    {
        protected override void ExecuteCommand(object param)
        {
            messenger.Send(new ResumeAllAlgorithmMessage());
        }
    }
}
