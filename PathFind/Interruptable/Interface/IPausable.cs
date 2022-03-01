using Interruptable.EventHandlers;

namespace Interruptable.Interface
{
    public interface IPausable
    {
        event ProcessEventHandler Paused;
        event ProcessEventHandler Resumed;

        bool IsPaused { get; }

        void Pause();
        void Resume();
    }
}