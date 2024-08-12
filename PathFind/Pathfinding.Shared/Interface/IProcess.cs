using Pathfinding.Shared.EventHandlers;

namespace Pathfinding.Shared.Interface
{
    public interface IProcess
    {
        event ProcessEventHandler Started;
        event ProcessEventHandler Finished;

        bool IsInProcess { get; }
    }
}
