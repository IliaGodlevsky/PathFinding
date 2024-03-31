using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    internal abstract class ImportGraphFromNetworkMenuItem<TImport>
        : ImportGraphMenuItem<int, TImport>
    {
        protected ImportGraphFromNetworkMenuItem(IMessenger messenger,
            IInput<int> input,
            ISerializer<IEnumerable<TImport>> serializer,
            ILog log,
            IService<Vertex> service)
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

        protected override IReadOnlyCollection<TImport> ImportGraph(int port)
        {
            Terminal.Write(Languages.WaitingForConnection);
            return serializer.DeserializeFromNetwork(port).ToReadOnly();
        }
    }
}
