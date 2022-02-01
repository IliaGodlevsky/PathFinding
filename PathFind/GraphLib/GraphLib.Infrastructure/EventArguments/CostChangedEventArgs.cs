using GraphLib.Interfaces;

namespace GraphLib.Infrastructure.EventArguments
{
    public class CostChangedEventArgs : BaseVertexChangedEventArgs<IVertexCost>
    {
        public CostChangedEventArgs(IVertex vertex) : base(vertex.Cost, vertex)
        {

        }
    }
}
