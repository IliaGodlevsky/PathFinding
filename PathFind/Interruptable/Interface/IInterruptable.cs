using Interruptable.EventHandlers;
using System.ComponentModel;

namespace Interruptable.Interface
{
    public interface IInterruptable
    {
        event InterruptEventHanlder OnInterrupted;

        [DefaultValue(false)]
        bool IsInterruptRequested { get; }

        void Interrupt();
    }
}