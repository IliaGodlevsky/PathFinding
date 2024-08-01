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
    [MediumPriority]
    internal sealed class SaveGraphToFileMenuItem(IFilePathInput input,
        IInput<int> intInput,
        ISerializer<IEnumerable<GraphSerializationModel>> serializer,
        ILog log,
        IRequestService<Vertex> service)
        : ExportGraphToFileMenuItem<GraphSerializationModel>(input, intInput, serializer, log, service)
    {

        public override string ToString()
        {
            return Languages.SaveGraph;
        }

        protected override async Task<GraphSerializationModel> GetForSave(int graphId,
            CancellationToken token)
        {
            return await service.ReadSerializationGraphAsync(graphId, token);
        }
    }
}
