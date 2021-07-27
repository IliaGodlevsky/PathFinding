﻿using Interruptable.EventHandlers;

namespace Interruptable.Interface
{
    /// <summary>
    /// Indicates that a main process 
    /// of the object can be interrupted
    /// </summary>
    public interface IInterruptable
    {
        /// <summary>
        /// Occures, when object's 
        /// main process is interrupted
        /// </summary>
        event InterruptEventHanlder OnInterrupted;

        /// <summary>
        /// Interrupts main process of the object 
        /// </summary>
        void Interrupt();
    }
}