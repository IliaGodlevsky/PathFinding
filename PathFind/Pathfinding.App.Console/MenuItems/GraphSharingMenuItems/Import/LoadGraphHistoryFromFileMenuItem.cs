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
    [HighestPriority]
    internal sealed class LoadGraphHistoryFromFileMenuItem(IMessenger messenger,
        IFilePathInput input,
        IPathfindingRangeBuilder<Vertex> rangeBuilder,
        ISerializer<IEnumerable<PathfindingHistorySerializationDto>> serializer,
        ILog log,
        IService<Vertex> service) : ImportGraphFromFileMenuItem<PathfindingHistorySerializationDto>(messenger, input, serializer, log, service)
    {
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;

        protected override GraphReadDto<Vertex> AddSingleImported(PathfindingHistorySerializationDto imported)
        {
            return service.AddPathfindingHistory(new[] { imported }).ElementAt(0).Graph;
        }

        protected override void AddImported(IEnumerable<PathfindingHistorySerializationDto> imported)
        {
            service.AddPathfindingHistory(imported);
        }

        public override string ToString()
        {
            return Languages.LoadGraphHistory;
        }

        protected override void Post(GraphReadDto<Vertex> dto)
        {
            var range = service.GetRange(dto.Id);
            rangeBuilder.Undo();
            rangeBuilder.Include(range, dto.Graph);
        }
    }
}
