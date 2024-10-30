using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphFieldView
    {
        private void Initialize()
        {
            X = 0;
            Y = 0;
            Width = Dim.Percent(66);
            Height = Dim.Fill();
            Border = new Border()
            {
                BorderBrush = Color.BrightYellow,
                BorderStyle = BorderStyle.Rounded,
                Title = "Graph field"
            };
        }
    }
}
