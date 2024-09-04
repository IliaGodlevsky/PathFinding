using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class NeighborhoodFactoryView
    {
        private readonly RadioGroup neighborhoods = new RadioGroup();

        private void Initialize()
        {
            X = Pos.Percent(35) + 1;
            Y = Pos.Percent(15) + 1;
            Width = Dim.Percent(35);
            Height = Dim.Percent(40);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Padding = new Thickness(0),
                Title = "Neighbors"
            };
            neighborhoods.X = 1;
            neighborhoods.Y = 1;
            Add(neighborhoods);
        }
    }
}
