namespace GraphLib.Interfaces
{
    public interface IVertexEventHolderFactory
    {
        IVertexEventHolder CreateVertexEventHolder(IGraph graph, IVertexCostFactory costFactory);
    }
}