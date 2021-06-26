using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// Indicates, that a class can be used as 
    /// a heuristic function for a pathfinding algorithm
    /// </summary>
    public interface IHeuristic
    {
        double Calculate(IVertex first, IVertex second);
    }
}