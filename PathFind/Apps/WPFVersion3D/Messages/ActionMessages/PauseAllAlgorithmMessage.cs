using WPFVersion3D.Interface;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.ActionMessages
{
    internal sealed class PauseAllAlgorithmMessage : IAlgorithmActionMessage
    {
        public void Execute(AlgorithmViewModel model)
        {
            model.Pause();
        }
    }
}
