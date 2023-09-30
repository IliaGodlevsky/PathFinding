using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class SaveGraphMenuItem : ExportGraphMenuItem<string>
    {
        public SaveGraphMenuItem(IFilePathInput input,
            IInput<int> intInput,
            GraphsPathfindingHistory history,
            ISerializer<GraphsPathfindingHistory> graphSerializer,
            ILog log)
            : base(input, intInput, history, graphSerializer, log)
        {
        }

        public override string ToString() => Languages.SaveGraph;


        protected override async Task ExportAsync(GraphsPathfindingHistory info, string path)
        {
            await graphSerializer.SerializeToFileAsync(info, path);
        }
    }
}
