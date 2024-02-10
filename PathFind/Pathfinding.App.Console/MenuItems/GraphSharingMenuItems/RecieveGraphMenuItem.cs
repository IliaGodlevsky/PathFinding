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
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class RecieveGraphMenuItem : ImportGraphMenuItem<int>
    {
        public RecieveGraphMenuItem(IMessenger messenger,
            IInput<int> input,
            IService service,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ISerializer<IEnumerable<PathfindingHistorySerializationDto>> serializer,
            ILog log)
            : base(messenger, input, service, rangeBuilder, serializer, log)
        {
        }

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
            return serializer.DeserializeFromNetwork(port).ToArray();
        }
    }
}
