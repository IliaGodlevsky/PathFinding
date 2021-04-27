using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    public interface IStepRule
    {
        double CalculateStepCost(IVertex neighbour, IVertex current);
    }
}