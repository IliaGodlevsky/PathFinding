using Pathfinding.Domain.Core;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal partial class AlgorithmsListView
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
            Width = Dim.Percent(30);
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
