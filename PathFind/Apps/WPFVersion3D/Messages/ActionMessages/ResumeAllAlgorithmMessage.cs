using WPFVersion3D.Interface;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.ActionMessages
{
    internal sealed class ResumeAllAlgorithmMessage : IAlgorithmActionMessage
    {
        public void Execute(AlgorithmViewModel model)
        {
            model.Resume();
        }
    }
}
