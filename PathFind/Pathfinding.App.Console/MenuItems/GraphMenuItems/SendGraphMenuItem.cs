using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class SendGraphMenuItem : ExportGraphMenuItem<(string Host, int Port)>
    {
        public SendGraphMenuItem(IInput<(string Host, int Port)> input,
            IInput<int> intInput,
            GraphsPathfindingHistory history,
            ISerializer<GraphsPathfindingHistory> graphSerializer,
            ILog log)
            : base(input, intInput, history, graphSerializer, log)
        {
        }

        public override string ToString() => Languages.SendGraph;


        protected override async Task ExportAsync(GraphsPathfindingHistory graph,
            (string Host, int Port) path)
        {
            await graphSerializer.SerializeToNetworkAsync(graph, path);
        }
    }
}
