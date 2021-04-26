namespace GraphLib.Interfaces.Factories
{
    public interface IVertexCostFactory
    {
        IVertexCost CreateCost();

        IVertexCost CreateCost(int cost);
    }
}
