using Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal sealed class OpenDijkstraAlgorithmCreateViewRequest : IOpenViewMessage
    {
        public void Open(Terminal.Gui.View view)
        {
            switch (view)
            {
                case CreateDijkstraRunView: view.Visible = true; break;
                default: view.Visible = false; break;
            }
        }
    }
}
