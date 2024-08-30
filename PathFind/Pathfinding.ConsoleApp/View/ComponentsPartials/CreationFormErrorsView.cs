using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class CreationFormErrorsView
    {
        private readonly TextField errors = new TextField();

        private void Initialize()
        {
            X = 1;
            Y = Pos.Percent(55) + 1;
            Width = Dim.Fill(1);
            Height = Dim.Percent(25);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                BorderBrush = Color.BrightRed,
                Padding = new Thickness(0),
                Title = "Errors"
            };
            errors.X = Pos.Center();
            errors.Y = Pos.Center();
            Add(errors);
        }
    }
}
