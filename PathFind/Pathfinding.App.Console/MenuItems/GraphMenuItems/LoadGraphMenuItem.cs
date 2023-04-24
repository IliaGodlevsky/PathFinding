using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class LoadGraphMenuItem : ImportGraphMenuItem<string>
    {
        public LoadGraphMenuItem(IMessenger messenger, IFilePathInput input,
            IGraphSerializer<Graph2D<Vertex>, Vertex> serializer, ILog log)
             : base(messenger, input, serializer, log)
        {

        }

        protected override Graph2D<Vertex> ImportGraph(string path)
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
