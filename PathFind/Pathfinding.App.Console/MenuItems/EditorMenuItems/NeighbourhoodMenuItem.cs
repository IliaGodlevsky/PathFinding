using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [MediumPriority]
    internal sealed class NeighbourhoodMenuItem : NavigateThroughVerticesMenuItem
    {
        public NeighbourhoodMenuItem(IInput<ConsoleKey> keyInput)
            : base(GetActions(), keyInput)
        {

        }

        private static VertexActions GetActions()
        {
            var activeVertex = new ActiveVertex();
            var exitAction = new ExitAction(activeVertex);
            var neighbourhoodAction = new NeighbourhoodAction(activeVertex);
            var actions = new (string, IVertexAction)[]
            {
                (nameof(Keys.DoNeighbourhoodAction), neighbourhoodAction),
                (nameof(Keys.ExitVerticesNavigating), exitAction),
                (nameof(Keys.LeaveVertexAction), exitAction)
            };
            return Array.AsReadOnly(actions);
        }

        public override string ToString()
        {
            return Languages.ChangeNeighbourhood;
        }
    }
}
