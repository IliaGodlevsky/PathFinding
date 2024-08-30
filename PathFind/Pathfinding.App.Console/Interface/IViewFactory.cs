using ReactiveUI;

namespace Pathfinding.App.Console.Interface
{
    public interface IViewFactory<out TView> 
        where TView : Terminal.Gui.View
    {
        TView CreateView();
    }
}
