using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
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
    internal abstract class ImportGraphFromNetworkMenuItem<TImport>
        : ImportGraphMenuItem<int, TImport>
    {
        protected ImportGraphFromNetworkMenuItem(IMessenger messenger,
            IInput<int> input,
            ISerializer<IEnumerable<TImport>> serializer,
            ILog log,
            IRequestService<Vertex> service)
            : base(messenger, input, serializer, log, service)
        {
        }

        protected override int InputPath()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input(Languages.InputPort);
            }
        }

        protected override async Task<IReadOnlyCollection<TImport>> ImportGraph(int port,
            CancellationToken token)
        {
            Terminal.Write(Languages.WaitingForConnection);
            return (await serializer.DeserializeFromNetwork(port, token)).ToReadOnly();
        }
    }
}
