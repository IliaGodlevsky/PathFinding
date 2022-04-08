using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class RemoveAlgorithmMessage : PassValueMessage<AlgorithmViewModel>
    {
        public RemoveAlgorithmMessage(AlgorithmViewModel value) : base(value)
        {
        }
    }
}
