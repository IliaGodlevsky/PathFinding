using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun.NewRunViews.AlgorithmSettingsViews
{
    internal sealed partial class HeuristicsView : FrameView
    {
        private readonly RadioGroup heuristics = new RadioGroup();

        private void Initialize()
        {
            heuristics.X = 1;
            heuristics.Y = 1;
            X = Pos.Percent(45);
            Y = 0;
            Height = Dim.Percent(33);
            Width = Dim.Percent(55);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Title = "Heuristics"
            };
            Visible = false;
            Add(heuristics);
        }
    }
}
