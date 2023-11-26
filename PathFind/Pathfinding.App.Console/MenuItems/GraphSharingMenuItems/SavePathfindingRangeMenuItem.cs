using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.DataAccess.ReadDto;
using Pathfinding.App.Console.DataAccess.Repo;
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
    internal sealed class SavePathfindingRangeMenuItem : IConditionedMenuItem
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;
        private readonly IDbContextService service;
        private readonly IInput<string> stringInput;
        private readonly IInput<int> intInput;
        private readonly ILog log;

        public SavePathfindingRangeMenuItem(ISerializer<IEnumerable<ICoordinate>> serializer,
            IDbContextService service,
            IFilePathInput stringInput,
            IInput<int> intInput,
            ILog log)
        {
            this.serializer = serializer;
            this.log = log;
            this.service = service;
            this.stringInput = stringInput;
            this.intInput = intInput;
        }

        public bool CanBeExecuted()
        {
            return service.GetAllGraphsInfo()
                .Select(i => service.GetPathfindingRange(i.Id))
                .Any(range => range.Any());
        }

        public async void Execute()
        {
            try
            {
                var allGraphs = service.GetAllGraphsInfo();
                if (allGraphs.Count == 1)
                {
                    var path = stringInput.Input();
                    var range = service.GetPathfindingRange(allGraphs.First().Id);
                    await serializer.SerializeToFileAsync(range, path);
                    return;
                }
                var graphs = allGraphs
                    .Where(graph => service.GetPathfindingRange(graph.Id).Any())
                    .ToArray();
                string menuList = CreateMenuList(graphs);
                int index = InputIndex(menuList, graphs.Length);
                if (index != graphs.Length)
                {
                    var path = stringInput.Input();
                    var range = service.GetPathfindingRange(graphs[index].Id);
                    await serializer.SerializeToFileAsync(range, path);
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

        private string CreateMenuList(IReadOnlyCollection<GraphReadDto> graphs)
        {
            return graphs.Select(k =>
            {
                return $"Width: {k.Width}\t Length: {k.Length}\t Obstacles: {k.ObstaclesCount}";
            })
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
        }

        public override string ToString()
        {
            return Languages.SaveRange;
        }
    }
}
