using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class SpreadView
    {
        private readonly RadioGroup spreadLevels = new();

        private void Initialize()
        {
            spreadLevels.X = 1;
            spreadLevels.Y = 1;
            X = 0;
            Y = Pos.Percent(33);
            Height = Dim.Percent(67);
            Width = Dim.Percent(45);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Title = "Spread"
            };
            Visible = false;
            Add(spreadLevels);
        }
    }
}
