using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    public interface IHeuristicFunction
    {
        double Calculate(IVertex first, IVertex second);
    }
}