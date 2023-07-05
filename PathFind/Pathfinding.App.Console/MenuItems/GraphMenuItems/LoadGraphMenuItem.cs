using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class LoadGraphMenuItem : ImportGraphMenuItem<string>
    {
        public LoadGraphMenuItem(IMessenger messenger,
            IFilePathInput input, GraphsPathfindingHistory history,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ISerializer<GraphsPathfindingHistory> serializer, ILog log)
            : base(messenger, input, history, rangeBuilder, serializer, log)
        {
        }

        protected override GraphsPathfindingHistory ImportGraph(string path)
        {
            return serializer.DeserializeFromFile(path);
        }

        protected override string InputPath()
        {
            return input.Input();
        }

        public override string ToString()
        {
            return Languages.LoadGraph;
        }
    }
}
