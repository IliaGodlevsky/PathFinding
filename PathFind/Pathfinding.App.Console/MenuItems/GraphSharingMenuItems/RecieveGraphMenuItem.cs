using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class RecieveGraphMenuItem(IMessenger messenger,
        IInput<int> input,
        IPathfindingRangeBuilder<Vertex> rangeBuilder,
        ISerializer<IEnumerable<PathfindingHistorySerializationDto>> serializer,
        ILog log,
        IService service) : ImportGraphMenuItem<int>(messenger, input, rangeBuilder, serializer, log, service)
    {
        public override string ToString()
        {
            return Languages.RecieveGraph;
        }

        protected override int InputPath()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input(Languages.InputPort);
            }
        }

        protected override IReadOnlyCollection<PathfindingHistorySerializationDto> ImportGraph(int port)
        {
            Terminal.Write(Languages.WaitingForConnection);
            return serializer.DeserializeFromNetwork(port).ToReadOnly();
        }
    }
}
