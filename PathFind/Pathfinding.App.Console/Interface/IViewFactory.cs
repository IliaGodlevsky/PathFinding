namespace Pathfinding.App.Console.Interface
{
    public interface IViewFactory<TView, TViewModel> 
        where TView : Terminal.Gui.View
        where TViewModel : class
    {
        TView CreateView();
    }
}
