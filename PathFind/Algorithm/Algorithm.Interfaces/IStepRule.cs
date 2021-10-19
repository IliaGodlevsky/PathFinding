using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// Represents the rule for 
    /// counting cost of pathfinding iteration
    /// </summary>
    public interface IStepRule
    {
        /// <summary>
        /// Calculates a cost of step between two given vertices
        /// </summary>
        /// <param name="neighbour"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        double CalculateStepCost(IVertex neighbour, IVertex current);
    }
}