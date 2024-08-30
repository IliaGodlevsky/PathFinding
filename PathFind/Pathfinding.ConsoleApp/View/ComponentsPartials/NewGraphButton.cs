using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class NewGraphButton
    {
        private void Initialize()
        {
            Text = "New";
            X = 0;
            Y = 0;
            Width = Dim.Percent(25);
        }
    }
}
