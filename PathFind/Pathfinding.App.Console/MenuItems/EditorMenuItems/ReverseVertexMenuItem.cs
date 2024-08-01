using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Settings;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Requests.Update;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [HighPriority]
    internal sealed class ReverseVertexMenuItem(IInput<ConsoleKey> keyInput,
        IRequestService<Vertex> service) : NavigateThroughVerticesMenuItem(keyInput, service)
    {
        public override async Task ExecuteAsync(CancellationToken token = default)
        {
            if (!token.IsCancellationRequested)
            {
                await base.ExecuteAsync(token);
                var vertices = processed.ToArray();
                var updatedVerticesRequest = new UpdateVerticesRequest<Vertex>()
                {
                    GraphId = graph.Id,
                    Vertices = processed.ToList()
                };
                var updateGraphInfoRequest = new UpdateGraphInfoRequest()
                {
                    Id = graph.Id,
                    Name = graph.Name,
                    ObstaclesCount = graph.Graph.GetObstaclesCount()
                };
                processed.Clear();
                await service.UpdateVerticesAsync(updatedVerticesRequest, token);
                await service.UpdateObstaclesCountAsync(updateGraphInfoRequest, token);
            }
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
