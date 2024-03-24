using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
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
    internal sealed class LoadGraphFromFileMenuItem(IMessenger messenger,
        IFilePathInput input,
        ISerializer<IEnumerable<GraphSerializationDto>> serializer,
        ILog log,
        IService service) : ImportGraphFromFileMenuItem<GraphSerializationDto>(messenger, input, serializer, log, service)
    {
        protected override void AddImported(IEnumerable<GraphSerializationDto> imported)
        {
            foreach (var import in imported)
            {
                service.AddGraph(import);
            }
        }

        protected override GraphReadDto AddSingleImported(GraphSerializationDto imported)
        {
            return service.AddGraph(imported);
        }

        public override string ToString()
        {
            return Languages.LoadGraph;
        }
    }
}
