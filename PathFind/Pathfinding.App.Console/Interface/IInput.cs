namespace Pathfinding.App.Console.Interface
{
    internal interface IInput<out T>
    {
        T Input();
    }
}
