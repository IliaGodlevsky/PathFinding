using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Export
{
    internal abstract class ExportGraphToFileMenuItem<TExport> : ExportGraphMenuItem<string, TExport>
    {
        protected ExportGraphToFileMenuItem(IFilePathInput input,
            IInput<int> intInput,
            ISerializer<IEnumerable<TExport>> graphSerializer,
            ILog log,
            IRequestService<Vertex> service) : base(input, intInput, graphSerializer, log, service)
        {
        }

        protected override async Task ExportAsync(string path,
            IEnumerable<TExport> histories,
            CancellationToken token)
        {
            await serializer.SerializeToFileAsync(histories, path, token);
        }
    }
}
