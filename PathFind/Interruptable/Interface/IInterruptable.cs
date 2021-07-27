using Interruptable.EventHandlers;
using System.ComponentModel;

namespace Interruptable.Interface
{
    public interface IInterruptable
    {
        event InterruptEventHanlder OnInterrupted;

        bool IsInterruptRequested { get; }

        /// <summary>
        /// Interrupts main process of the object 
        /// </summary>
        /// Interrupts main process of the object 
        /// </summary>
        /// Interrupts main process of the object 
        /// </summary>
        /// Interrupts main process of the object 
        /// </summary>
        /// Interrupts main process of the object 
        /// </summary>
        void Interrupt();
    }
}