namespace GraphLib.Interfaces
{
    public interface IMeanCost
    {
        int Calculate(IVertex neighbour, IVertex vertex);
    }
}
