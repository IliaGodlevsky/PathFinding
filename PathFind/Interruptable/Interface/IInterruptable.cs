using Interruptable.EventHandlers;
using System.ComponentModel;

namespace Interruptable.Interface
{
    public interface IInterruptable
    {
        event InterruptEventHanlder OnInterrupted;

        void Interrupt();
    }
}