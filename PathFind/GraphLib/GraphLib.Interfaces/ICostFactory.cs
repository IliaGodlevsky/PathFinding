namespace GraphLib.Interface
{
    public interface IVertexCostFactory
    {
        IVertexCost CreateCost();

        IVertexCost CreateCost(int cost);
    }
}
