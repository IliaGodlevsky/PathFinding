using Interruptable.EventHandlers;

namespace Interruptable.Interface
{
    public interface IInterruptableProcess : IInterruptable
    {
        event ProcessEventHandler Started;
        event ProcessEventHandler Finished;
    }
}
