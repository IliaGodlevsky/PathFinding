namespace GraphLib.Interfaces.Factories
{
    public interface IVertexCostFactory
    {
        IVertexCost CreateCost(int cost);

        IVertexCost CreateCost();
    }
}
