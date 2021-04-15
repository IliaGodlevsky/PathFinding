namespace GraphLib.Interfaces
{
    public interface IVertexCostFactory
    {
        IVertexCost CreateCost();

        IVertexCost CreateCost(int cost);
    }
}
