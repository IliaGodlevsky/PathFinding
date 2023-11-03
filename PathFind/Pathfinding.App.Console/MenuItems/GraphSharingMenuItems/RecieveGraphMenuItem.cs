using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class RecieveGraphMenuItem : ImportGraphMenuItem<int>
    {
        public RecieveGraphMenuItem(IMessenger messenger,
            IInput<int> input, GraphsPathfindingHistory history,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ISerializer<GraphsPathfindingHistory> serializer,
            ILog log)
            : base(messenger, input, history, rangeBuilder, serializer, log)
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

        protected override GraphsPathfindingHistory ImportGraph(int port)
        {
            Terminal.Write(Languages.WaitingForConnection);
            return serializer.DeserializeFromNetwork(port);
        }
    }
}
