using Shared.Process.EventHandlers;

namespace Shared.Process.Interface
{
    public interface IInterruptable
    {
        event ProcessEventHandler Interrupted;

        void Interrupt();
    }
}