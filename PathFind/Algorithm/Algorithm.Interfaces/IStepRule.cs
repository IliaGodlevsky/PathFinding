using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// Represents the rule for 
    /// counting cost of pathfinding iteration
    /// </summary>
    public interface IStepRule
    {
        double CalculateStepCost(IVertex neighbour, IVertex current);
    }
}