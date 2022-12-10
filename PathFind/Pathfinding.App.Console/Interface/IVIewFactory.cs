using Pathfinding.App.Console.Views;

namespace Pathfinding.App.Console.Interface
{
    internal interface IViewFactory
    {
        View CreateView(IUnit unit);
    }
}
