using WPFVersion.ViewModel;

namespace WPFVersion.Messages.ActionMessages
{
    internal sealed class RemoveAlgorithmMessage
    {
        public AlgorithmViewModel Model { get; }

        public RemoveAlgorithmMessage(AlgorithmViewModel model)
        {
            Model = model;
        }
    }
}
