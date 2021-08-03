using Interruptable.EventHandlers;

namespace Interruptable.Interface
{
    public interface IInterruptable
    {
        event InterruptEventHanlder Interrupted;

        void Interrupt();
    }
}