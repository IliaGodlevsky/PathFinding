using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Factory.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [MediumPriority]
    internal sealed class NeighbourhoodMenuItem : SwitchVerticesMenuItem
    {
        public NeighbourhoodMenuItem(IInput<ConsoleKey> keyInput, 
            INeighborhoodFactory factory) 
            : base(GetActions(factory), keyInput)
        {

        }

        private static VertexActions GetActions(INeighborhoodFactory factory)
        {
            var activeVertex = new ActiveVertex();
            var exitAction = new ExitAction(activeVertex);
            var neighbourhoodAction = new NeighbourhoodAction(activeVertex, factory);
            var result = new (string, IVertexAction)[]
            {
                (nameof(Keys.NeighbourhoodAction), neighbourhoodAction),
                (nameof(Keys.ExitVertexSwitching), exitAction),
                (nameof(Keys.ExitVertexAction), exitAction)
            };
            return Array.AsReadOnly(result);
        }

        public override string ToString()
        {
            return "Change neighbourhood";
        }
    }
}
