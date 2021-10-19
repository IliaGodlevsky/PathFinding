using Interruptable.EventHandlers;

namespace Interruptable.Interface
{
    public interface IPausableProcess
    {
        event ProcessEventHandler Paused;
        event ProcessEventHandler Resumed;

        void Pause();

        void Resume();
    }
}
