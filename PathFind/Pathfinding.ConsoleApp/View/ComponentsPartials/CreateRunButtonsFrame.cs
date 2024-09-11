using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed partial class RunCreationButtonsFrame
    {
        private void Initialize()
        {
            X = 0;
            Y = Pos.Percent(90);
            Width = Dim.Fill();
            Height = Dim.Fill();
            Visible = false;
        }
    }
}
