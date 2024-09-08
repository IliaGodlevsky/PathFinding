using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.ButtonFrame
{
    internal partial class NewRunButton
    {
        private void Initialize()
        {
            Text = "New";
            X = Pos.Percent(15);
            Y = 0;
            Width = Dim.Percent(25);
        }
    }
}
