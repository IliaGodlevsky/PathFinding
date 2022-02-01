using GraphLib.Infrastructure.EventHandlers;

namespace GraphLib.Infrastructure.Interfaces
{
    public interface INotifyVertexCostChanged
    {
        event CostChangedEventHandler CostChanged;
    }
}
