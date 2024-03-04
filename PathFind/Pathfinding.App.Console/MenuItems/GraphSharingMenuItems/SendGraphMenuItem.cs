using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class SendGraphMenuItem(IInput<(string Host, int Port)> input,
        IInput<int> intInput,
        ISerializer<IEnumerable<PathfindingHistorySerializationDto>> graphSerializer,
        ILog log,
        IService service) : ExportGraphMenuItem<(string Host, int Port)>(input, intInput, graphSerializer, log, service)
    {
        public override string ToString() => Languages.SendGraph;

        protected override async Task ExportAsync((string Host, int Port) path, IEnumerable<PathfindingHistorySerializationDto> graph)
        {
            await graphSerializer.SerializeToNetworkAsync(graph, path);
        }
    }
}
