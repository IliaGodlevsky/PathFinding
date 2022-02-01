using GraphLib.Infrastructure.EventArguments;
using GraphLib.Interfaces;

namespace GraphLib.Infrastructure.EventHandlers
{
    public delegate void CostChangedEventHandler(object sender, BaseVertexChangedEventArgs<IVertexCost> e);
}
