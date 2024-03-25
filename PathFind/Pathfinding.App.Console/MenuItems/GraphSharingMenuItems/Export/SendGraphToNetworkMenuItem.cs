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
    [LowPriority]
    internal class SendGraphToNetworkMenuItem(IInput<(string Host, int Port)> input,
        IInput<int> intInput,
        ISerializer<IEnumerable<GraphSerializationDto>> serializer,
        ILog log,
        IService service) : ExportGraphToNetworkMenuItem<GraphSerializationDto>(input, intInput, serializer, log, service)
    {
        protected override GraphSerializationDto GetForSave(int graphId)
        {
            return service.GetSerializationGraph(graphId);
        }

        public override string ToString() => Languages.SendGraph;
    }
}
