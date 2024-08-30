using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class LoadGraphButton
    {
        private void Initialize()
        {
            Text = "Load";
            Y = 0;
            X = Pos.Percent(50);
            Width = Dim.Percent(25);
        }
    }
}