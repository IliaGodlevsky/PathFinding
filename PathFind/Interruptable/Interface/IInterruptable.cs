using Interruptable.EventHandlers;

namespace Interruptable.Interface
{
    public interface IInterruptable
    {
        event ProcessEventHandler Interrupted;

        void Interrupt();
    }
}