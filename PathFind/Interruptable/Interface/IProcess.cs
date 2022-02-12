using Interruptable.EventHandlers;

namespace Interruptable.Interface
{
    public interface IProcess
    {
        event ProcessEventHandler Started;
        event ProcessEventHandler Finished;

        bool IsInProcess { get; }
    }
}
