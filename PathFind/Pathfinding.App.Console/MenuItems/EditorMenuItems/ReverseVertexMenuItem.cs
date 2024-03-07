using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [HighPriority]
    internal sealed class ReverseVertexMenuItem(IInput<ConsoleKey> keyInput,
        IService service) : NavigateThroughVerticesMenuItem(keyInput, service)
    {
        public override async void Execute()
        {
            base.Execute();
            var vertices = processed.ToArray();
            processed.Clear();
            int obstacles = graph.Graph.GetObstaclesCount();
            await Task.Run(() =>
            {
                service.UpdateVertices(processed, graph.Id);
                service.UpdateObstaclesCount(obstacles, graph.Id);
            });
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
