namespace Pathfinding.App.Console.Interface
{
    internal interface IMenuItem
    {
        int Order { get; }

        void Execute();

        bool CanBeExecuted();
    }
}
