using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    [LowPriority]
    internal sealed class RecieveGraphHistoryFromNetworkMenuItem(IMessenger messenger,
        IInput<int> input,
        IPathfindingRangeBuilder<Vertex> rangeBuilder,
        ISerializer<IEnumerable<PathfindingHistorySerializationDto>> serializer,
        ILog log,
        IService service) : ImportGraphFromNetworkMenuItem<PathfindingHistorySerializationDto>(messenger, input, serializer, log, service)
    {
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;

        protected override GraphReadDto AddSingleImported(PathfindingHistorySerializationDto imported)
        {
            var result = service.AddPathfindingHistory(new[] { imported }).ElementAt(0);
            rangeBuilder.Undo();
            rangeBuilder.Include(result.Range, result.Graph);
            return new() { Graph = result.Graph, Id = result.Id };
        }

        protected override void AddImported(IEnumerable<PathfindingHistorySerializationDto> imported)
        {
            service.AddPathfindingHistory(imported);
        }

        public override string ToString()
        {
            return Languages.RecieveGraph;
        }
    }
}
