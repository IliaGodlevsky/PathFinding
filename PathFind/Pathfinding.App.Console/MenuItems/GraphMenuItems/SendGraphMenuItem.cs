using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class SendGraphMenuItem : ExportGraphMenuItem<(string Host, int Port)>
    {
        public SendGraphMenuItem(IMessenger messenger, IInput<(string Host, int Port)> input,
            IGraphSerializer<Graph2D<Vertex>, Vertex> serializer, ILog log)
            : base(messenger, input, serializer, log)
        {
        }

        public override string ToString()
        {
            return Languages.SendGraph;
        }

        protected override async Task ExportAsync((string Host, int Port) path)
        {
            await serializer.SerializeToNetworkAsync(graph, path);
        }
    }
}
