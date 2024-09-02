using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphFieldView
    {
        private void Initialize()
        {
            X = 0;
            Y = 0;
            Width = Dim.Percent(75);
            Height = Dim.Fill();
            Border = new Border();
        }
    }
}
