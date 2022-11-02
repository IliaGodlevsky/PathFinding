using Shared.Process.EventHandlers;

namespace Shared.Process.Interface
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