using Shared.Process.EventHandlers;

namespace Shared.Process.Interface
{
    public interface IProcess
    {
        event ProcessEventHandler Started;
        event ProcessEventHandler Finished;

        bool IsInProcess { get; }
    }
}
