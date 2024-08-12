using Pathfinding.Shared.EventHandlers;

namespace Pathfinding.Shared.Interface
{
    public interface IInterruptable
    {
        event ProcessEventHandler Interrupted;

        void Interrupt();
    }
}