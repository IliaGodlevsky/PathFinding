using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Export
{
    internal abstract class ExportGraphToFileMenuItem<TExport> : ExportGraphMenuItem<string, TExport>
    {
        protected ExportGraphToFileMenuItem(IFilePathInput input,
            IInput<int> intInput,
            ISerializer<IEnumerable<TExport>> graphSerializer,
            ILog log,
            IService<Vertex> service) : base(input, intInput, graphSerializer, log, service)
        {
        }

        protected override async Task ExportAsync(string path, IEnumerable<TExport> histories)
        {
            await serializer.SerializeToFileAsync(histories, path);
        }
    }
}
