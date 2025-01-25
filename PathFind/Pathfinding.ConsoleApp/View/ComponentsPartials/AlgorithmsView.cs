using Pathfinding.ConsoleApp.Resources;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal partial class AlgorithmsView
    {
        private readonly ListView algorithms = new();

        private void Initialize()
        {
            algorithms.X = 0;
            algorithms.Y = 0;
            algorithms.Width = Dim.Fill();
            algorithms.Height = Dim.Fill();
            X = 0;
            Y = 1;
            Width = Dim.Percent(25);
            Height = Dim.Percent(80);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Title = Resource.Algorithms,
                BorderThickness = new Thickness(0)
            };
            Add(algorithms);
        }
    }
}
