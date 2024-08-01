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
    internal class SendGraphToNetworkMenuItem(IInput<(string Host, int Port)> input,
        IInput<int> intInput,
        ISerializer<IEnumerable<GraphSerializationModel>> serializer,
        ILog log,
        IRequestService<Vertex> service) : ExportGraphToNetworkMenuItem<GraphSerializationModel>(input, intInput, serializer, log, service)
    {
        protected override async Task<GraphSerializationModel> GetForSave(int graphId,
            CancellationToken token)
        {
            return await service.ReadSerializationGraphAsync(graphId, token);
        }

        public override string ToString() => Languages.SendGraph;
    }
}
