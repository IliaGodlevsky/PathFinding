namespace Pathfinding.App.Console.Interface
{
    internal interface IMenuItem : IAction
    {
        bool CanBeExecuted();
    }
}
