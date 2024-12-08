using System.Globalization;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class HeuristicsView : FrameView
    {
        private readonly RadioGroup heuristics = new RadioGroup();

        private void Initialize()
        {
            heuristics.X = 1;
            heuristics.Y = 0;
            X = 0;
            Y = Pos.Percent(20) + 1;
            Height = Dim.Percent(35);
            Width = Dim.Percent(30);
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
