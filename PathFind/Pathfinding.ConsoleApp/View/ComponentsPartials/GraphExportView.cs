using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphExportView : Button
    {
        private void Initialize()
        {
            X = Pos.Percent(50.01f);
            Y = 0;
            Width = Dim.Percent(16.67f);
            Text = "Save";
        }
    }
}
