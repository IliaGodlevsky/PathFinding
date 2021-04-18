using System;

namespace Common.Interface
{
    public interface IInterruptable
    {
        event EventHandler OnInterrupted;

        void Interrupt();
    }
}