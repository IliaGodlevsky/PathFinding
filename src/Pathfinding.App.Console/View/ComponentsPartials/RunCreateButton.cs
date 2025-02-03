using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal partial class RunCreateButton
    {
        private void Initialize()
        {
            Text = "New";
            X = Pos.Percent(0);
            Y = 0;
            Width = Dim.Percent(33);
        }
    }
}
