using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphImportView
    {
        private void Initialize()
        {
            Text = "Load";
            Y = 0;
            X = Pos.Percent(50);
            Width = Dim.Percent(25);
        }
    }
}