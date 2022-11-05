using Algorithm.Base;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class AlgorithmStartedMessage : PassValueMessage<PathfindingProcess>
    {
        public AlgorithmStartedMessage(PathfindingProcess algorithm) : base(algorithm)
        {

        }
    }
}
