using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Serialization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Export
{
    [LowPriority]
    internal sealed class SendGraphHistoryToNetworkMenuItem(IInput<(string Host, int Port)> input,
        IInput<int> intInput,
        ISerializer<IEnumerable<PathfindingHistorySerializationModel>> serializer,
        ILog log,
        IRequestService<Vertex> service) : ExportGraphToNetworkMenuItem<PathfindingHistorySerializationModel>(input, intInput, serializer, log, service)
    {
        public override string ToString() => Languages.SendGraphHistory;

        protected override async Task<PathfindingHistorySerializationModel> GetForSave(int graphId,
            CancellationToken token)
        {
            return await service.ReadSerializationHistoryAsync(graphId, token);
        }
    }
}
