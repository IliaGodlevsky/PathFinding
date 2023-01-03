namespace Pathfinding.App.Console.Interface
{
    internal interface IConditionedMenuItem : IMenuItem
    {
        bool CanBeExecuted();
    }
}
