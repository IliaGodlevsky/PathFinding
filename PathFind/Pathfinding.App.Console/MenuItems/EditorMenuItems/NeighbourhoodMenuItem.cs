using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using System;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [MediumPriority]
    internal sealed class NeighbourhoodMenuItem : SwitchVerticesMenuItem
    {
        public NeighbourhoodMenuItem(IInput<ConsoleKey> keyInput) 
            : base(GetActions(), keyInput)
        {

        }

        private static VertexActions GetActions()
        {
            var activeVertex = new ActiveVertex();
            var exitAction = new ExitAction(activeVertex);
            var factory = new MooreNeighborhoodFactory();
            var neighbourhoodAction = new NeighbourhoodAction(activeVertex, factory);
            var actions = new (string, IVertexAction)[]
            {
                (nameof(Keys.NeighbourhoodAction), neighbourhoodAction),
                (nameof(Keys.ExitVertexSwitching), exitAction),
                (nameof(Keys.ExitVertexAction), exitAction)
            };
            return Array.AsReadOnly(actions);
        }

        public override string ToString()
        {
            return Languages.ChangeNeighbourhood;
        }
    }
}
