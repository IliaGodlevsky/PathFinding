using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    internal abstract class ImportGraphFromFileMenuItem<TImport>
        : ImportGraphMenuItem<string, TImport>
    {
        protected ImportGraphFromFileMenuItem(IMessenger messenger,
            IFilePathInput input,
            ISerializer<IEnumerable<TImport>> serializer,
            ILog log,
            IRequestService<Vertex> service)
            : base(messenger, input, serializer, log, service)
        {
        }

        protected override sealed async Task<IReadOnlyCollection<TImport>> ImportGraph(string path,
            CancellationToken token)
        {
            return (await serializer.DeserializeFromFileAsync(path, token)).ToReadOnly();
        }

        protected override sealed string InputPath()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input();
            }
        }
    }
}
