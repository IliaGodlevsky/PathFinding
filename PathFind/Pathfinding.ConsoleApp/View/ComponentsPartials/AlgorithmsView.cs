using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal partial class AlgorithmsView
    {
        private readonly RadioGroup algorithms = new RadioGroup();

        private void Initialize()
        {
            algorithms.X = 1;
            algorithms.Y = 1;
            algorithms.Width = Dim.Fill();
            algorithms.Height = Dim.Fill();
            X = 0;
            Y = 0;
            Width = Dim.Percent(33);
            Height = Dim.Percent(87);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Title = "Algorithms",
                BorderThickness = new Thickness(0)
            };
            Add(algorithms);
        }
    }
}
