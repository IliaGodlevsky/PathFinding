using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed partial class CreateRunButtonsFrame
    {
        private void Initialize()
        {
            X = Pos.Percent(30) + 1;
            Y = Pos.Percent(80) + 1;
            Width = Dim.Fill();
            Height = Dim.Percent(20);
            Visible = false;
        }
    }
}
