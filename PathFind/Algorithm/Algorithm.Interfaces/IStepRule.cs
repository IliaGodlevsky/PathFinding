using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    public interface IStepRule
    {
        double CountStepCost(IVertex neighbour, IVertex current);
    }
}