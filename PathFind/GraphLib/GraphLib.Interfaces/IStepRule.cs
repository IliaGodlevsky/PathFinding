namespace GraphLib.Interfaces
{
    public interface IStepRule
    {
        int StepCost(IVertex neighbour, IVertex current);
    }
}