﻿using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class SaveGraphMenuItem(IFilePathInput input,
        IInput<int> intInput,
        ISerializer<IEnumerable<PathfindingHistorySerializationDto>> graphSerializer,
        ILog log,
        IService service)
        : ExportGraphMenuItem<string>(input, intInput, graphSerializer, log, service)
    {
        public override string ToString() => Languages.SaveGraph;

        protected override async Task ExportAsync(string path, IEnumerable<PathfindingHistorySerializationDto> info)
        {
            await graphSerializer.SerializeToFileAsync(info, path);
        }
    }
}
