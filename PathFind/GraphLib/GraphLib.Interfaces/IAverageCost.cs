namespace GraphLib.Interfaces
{
    public interface IAverageCost
    {
        int Calculate(IVertex neighbour, IVertex vertex);
    }
}
