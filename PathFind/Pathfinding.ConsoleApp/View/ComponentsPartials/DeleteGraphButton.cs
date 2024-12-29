using Pathfinding.ConsoleApp.Resources;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class DeleteGraphButton
    {
        private void Initialize()
        {
            Text = Resource.DeleteGraph;
            X = Pos.Percent(83.65f);
            Y = 0;
            Width = Dim.Percent(16.65f);
        }
    }
}
