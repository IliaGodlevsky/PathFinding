using Pathfinding.Shared.EventHandlers;

namespace Pathfinding.Shared.Interface
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