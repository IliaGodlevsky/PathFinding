using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    public interface IHeuristic
    {
        double Calculate(IVertex first, IVertex second);
    }
}