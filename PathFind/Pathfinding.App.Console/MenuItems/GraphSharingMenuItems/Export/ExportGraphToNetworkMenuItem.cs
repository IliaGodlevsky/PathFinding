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
    internal abstract class ExportGraphToNetworkMenuItem<TExport> : ExportGraphMenuItem<(string Host, int Port), TExport>
    {
        protected ExportGraphToNetworkMenuItem(IInput<(string Host, int Port)> input,
            IInput<int> intInput, ISerializer<IEnumerable<TExport>> graphSerializer, 
            ILog log,
            IService<Vertex> service) : base(input, intInput, graphSerializer, log, service)
        {
        }

        protected override async Task ExportAsync((string Host, int Port) path, IEnumerable<TExport> histories)
        {
            await serializer.SerializeToNetworkAsync(histories, path);
        }
    }
}
