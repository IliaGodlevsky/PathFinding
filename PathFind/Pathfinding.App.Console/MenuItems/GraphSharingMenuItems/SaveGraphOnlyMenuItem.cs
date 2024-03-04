using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class SaveGraphOnlyMenuItem(IFilePathInput input,
        IInput<int> intInput,
        ISerializer<GraphSerializationDto> serializer,
        ILog log,
        IService service) : IConditionedMenuItem
    {
        private readonly IInput<string> stringInput = input;
        private readonly IInput<int> intInput = intInput;
        private readonly IService service = service;
        private readonly ISerializer<GraphSerializationDto> serializer = serializer;
        private readonly ILog log = log;

        public bool CanBeExecuted()
        {
            return service is not null && service.GetGraphCount() > 0;
        }

        public async void Execute()
        {
            try
            {
                var info = service.GetAllGraphInfo();
                if (info.Count == 1)
                {
                    var path = stringInput.Input();
                    int id = info[0].Id;
                    var graph = service.GetSerializationGraph(id);
                    await serializer.SerializeToFileAsync(graph, path);
                    return;
                }
                string menuList = CreateMenuList(info);
                int index = InputIndex(menuList, info.Count);
                if (index != info.Count)
                {
                    var path = stringInput.Input();
                    var graph = service.GetSerializationGraph(info[index].Id);
                    await serializer.SerializeToFileAsync(graph, path);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private int InputIndex(string message, int count)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = intInput.Input(message, count + 1, 1) - 1;
                return index;
            }
        }

        private string CreateMenuList(IReadOnlyCollection<GraphEntity> graphs)
        {
            return graphs.Select(k => k.ConvertToString())
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
        }

        public override string ToString()
        {
            return Languages.SaveGraphOnly;
        }
    }
}
