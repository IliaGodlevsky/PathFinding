using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// Indicates, that a class can be used as 
    /// a heuristic function for a pathfinding algorithm
    /// </summary>
    public interface IHeuristic
    {
        /// <summary>
        /// Calculates the value of a heuristic 
        /// function for two given vertices
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        double Calculate(IVertex first, IVertex second);
    }
}