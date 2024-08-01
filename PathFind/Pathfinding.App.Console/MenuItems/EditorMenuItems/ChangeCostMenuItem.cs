using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Settings;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Requests.Update;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [LowPriority]
    internal sealed class ChangeCostMenuItem(IInput<ConsoleKey> keyInput,
        IRequestService<Vertex> service) : NavigateThroughVerticesMenuItem(keyInput, service)
    {
        public async override Task ExecuteAsync(CancellationToken token = default)
        {
            if (!token.IsCancellationRequested)
            {
                await base.ExecuteAsync(token);
                var request = new UpdateVerticesRequest<Vertex>()
                {
                    GraphId = graph.Id,
                    Vertices = processed.ToList()
                };
                processed.Clear();
                await service.UpdateVerticesAsync(request, token);
            }
        }

        public override string ToString()
        {
            return Languages.ChangeCost;
        }

        protected override VertexActions GetActions()
        {
            return new (string, IVertexAction)[]
            {
                (nameof(Keys.Default.IncreaseCost), new IncreaseCostAction()),
                (nameof(Keys.Default.DecreaseCost), new DecreaseCostAction())
            };
        }
    }
}
