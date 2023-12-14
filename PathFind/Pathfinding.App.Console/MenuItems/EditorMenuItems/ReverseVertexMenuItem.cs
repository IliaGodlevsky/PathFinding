using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.Services;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [HighPriority]
    internal sealed class ReverseVertexMenuItem : NavigateThroughVerticesMenuItem
    {
        public ReverseVertexMenuItem(IInput<ConsoleKey> keyInput, IService service)
            : base(keyInput, service)
        {
        }

        public override void Execute()
        {
            base.Execute();
            service.UpdateVertices(processed, id);
            processed.Clear();
            int obstacles = graph.GetObstaclesCount();
            service.UpdateObstaclesCount(obstacles, id);
        }

        protected override VertexActions GetActions()
        {
            return new (string, IVertexAction)[]
            {
                (nameof(Keys.Default.ReverseVertex), new ReverseVertexAction())
            };
        }

        public override string ToString()
        {
            return Languages.ReverseVertices;
        }
    }
}
