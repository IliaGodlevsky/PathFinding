using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    [LowPriority]
    internal sealed class ReceiveGraphHistoryFromNetworkMenuItem(IMessenger messenger,
        IInput<int> input,
        IPathfindingRangeBuilder<Vertex> rangeBuilder,
        ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer,
        ILog log,
        IRequestService<Vertex> service) : ImportGraphFromNetworkMenuItem<PathfindingHistorySerializationModel>(messenger, input, serializer, log, service)
    {
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;

        protected override async Task<GraphModel<Vertex>> AddSingleImported(PathfindingHistorySerializationModel imported,
            CancellationToken token)
        {
            return (await service.CreatePathfindingHistoryAsync(new[] { imported }, token))
                .Models.ElementAt(0)
                .Graph;
        }

        protected override async Task AddImported(IEnumerable<PathfindingHistorySerializationModel> imported,
            CancellationToken token)
        {
            await service.CreatePathfindingHistoryAsync(imported, token);
        }

        protected override async Task Post(GraphModel<Vertex> model, CancellationToken token)
        {
            var range = await service.ReadRangeAsync(model.Id, token);
            rangeBuilder.Undo();
            rangeBuilder.Include(range.Range, model.Graph);
        }

        public override string ToString()
        {
            return Languages.RecieveGraphHistory;
        }
    }
}
