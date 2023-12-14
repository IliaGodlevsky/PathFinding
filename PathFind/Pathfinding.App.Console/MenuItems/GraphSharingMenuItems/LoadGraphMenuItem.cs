using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Mappers;
using Pathfinding.App.Console.DataAccess.Services;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class LoadGraphMenuItem : ImportGraphMenuItem<string>
    {
        public LoadGraphMenuItem(IMessenger messenger,
            IFilePathInput input, IService service,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IMapper mapper,
            ISerializer<IEnumerable<PathfindingHistorySerializationDto>> serializer,
            ILog log)
            : base(messenger, input, service, mapper, rangeBuilder, serializer, log)
        {
        }

        protected override IReadOnlyCollection<PathfindingHistorySerializationDto> ImportGraph(string path)
        {
            return serializer.DeserializeFromFile(path).ToArray();
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
