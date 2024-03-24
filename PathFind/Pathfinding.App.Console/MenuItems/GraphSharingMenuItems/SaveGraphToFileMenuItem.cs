using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class SaveGraphToFileMenuItem(IFilePathInput input,
        IInput<int> intInput,
        ISerializer<IEnumerable<GraphSerializationDto>> serializer,
        ILog log,
        IService service) 
        : ExportGraphToFileMenuItem<GraphSerializationDto>(input, intInput, serializer, log, service)
    {

        public override string ToString()
        {
            return Languages.SaveGraph;
        }

        protected override GraphSerializationDto GetForSave(int graphId)
        {
            return service.GetSerializationGraph(graphId);
        }
    }
}
