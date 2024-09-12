using System.Reflection;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class DeleteGraphButton
    {
        private void Initialize()
        {
            Text = "Delete";
            X = Pos.Percent(75);
            Y = 0;
            Width = Dim.Percent(25);
        }
    }
}
