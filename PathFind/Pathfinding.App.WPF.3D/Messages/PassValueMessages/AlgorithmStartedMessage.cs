using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class AlgorithmStartedMessage : PassValueMessage<PathfindingProcess>
    {
        public AlgorithmStartedMessage(PathfindingProcess algorithm) : base(algorithm)
        {

        }
    }
}
