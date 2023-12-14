using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.Services;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class SaveGraphOnlyMenuItem : IConditionedMenuItem
    {
        private readonly IInput<string> stringInput;
        private readonly IInput<int> intInput;
        private readonly IService service;
        private readonly ISerializer<IGraph<Vertex>> serializer;
        private readonly ILog log;

        public SaveGraphOnlyMenuItem(IFilePathInput input,
            IInput<int> intInput,
            IService service,
            ISerializer<IGraph<Vertex>> serializer,
            ILog log)
        {
            this.stringInput = input;
            this.intInput = intInput;
            this.service = service;
            this.serializer = serializer;
            this.log = log;
        }

        public bool CanBeExecuted()
        {
            return service.GetGraphIds().Count > 0;
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
                    var graph = service.GetGraph(id);
                    await serializer.SerializeToFileAsync(graph, path);
                    return;
                }
                string menuList = CreateMenuList(info);
                int index = InputIndex(menuList, info.Count);
                if (index != info.Count)
                {
                    var path = stringInput.Input();
                    var graph = service.GetGraph(info[index].Id);
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

        private string GetString(int width, int length, int obstacles)
        {
            int count = width * length;
            int obstaclePercent = obstacles / count;
            return $"Width: {width} Length: {length} Obstacle percent: {obstaclePercent}({obstacles}/{count})";
        }

        public override string ToString()
        {
            return Languages.SaveGraphOnly;
        }
    }
}
