using System;

namespace Common.Interface
{
    public interface IInterrupted
    {
        event EventHandler OnInterrupted;

        void Interrupt();
    }
}