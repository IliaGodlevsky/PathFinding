using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Export
{
    [MediumPriority]
    internal sealed class SaveGraphHistoryToFileMenuItem(IFilePathInput input,
        IInput<int> intInput,
        ISerializer<IEnumerable<PathfindingHistorySerializationDto>> serializer,
        ILog log,
        IService service)
        : ExportGraphToFileMenuItem<PathfindingHistorySerializationDto>(input, intInput, serializer, log, service)
    {
        protected override PathfindingHistorySerializationDto GetForSave(int graphId)
        {
            return service.GetSerializationHistory(graphId);
        }

        public override string ToString() => Languages.SaveGraphHistory;
    }
}
