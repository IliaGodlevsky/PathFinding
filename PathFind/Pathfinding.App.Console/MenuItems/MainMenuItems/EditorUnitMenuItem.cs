using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    [MediumPriority]
    internal sealed class EditorUnitMenuItem : MainMenuItem<GraphEditorUnit>
    {
        public EditorUnitMenuItem(IViewFactory viewFactory, GraphEditorUnit viewModel,
            IMessenger messenger, ILog log)
            : base(viewFactory, viewModel, messenger, log)
        {

        }

        public override string ToString()
        {
            return Languages.Editor;
        }
    }
}
