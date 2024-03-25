using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Export
{
    [LowPriority]
    internal sealed class SavePathfindingRangeMenuItem : IConditionedMenuItem
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;
        private readonly IService service;
        private readonly IInput<string> stringInput;
        private readonly IInput<int> intInput;
        private readonly ILog log;

        public SavePathfindingRangeMenuItem(ISerializer<IEnumerable<ICoordinate>> serializer,
            IService service,
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
            var ids = service.GetGraphIds();
            return ids.Select(id => service.GetRange(id)).Any(r => r.Count > 0);
        }

        public async void Execute()
        {
            try
            {
                var ids = service.GetGraphIds();
                if (ids.Count == 1)
                {
                    var path = stringInput.Input();
                    var range = service.GetRange(ids.First());
                    await serializer.SerializeToFileAsync(range, path);
                    return;
                }
                var graphsWithPath = ids
                    .Where(id => service.GetRange(id).Count > 0)
                    .ToArray();
                var ranges = ids
                    .Select(id => service.GetRange(id))
                    .ToArray();
                var graphs = service.GetAllGraphInfo()
                    .Where(x => ids.Contains(x.Id))
                    .ToArray();
                string menuList = CreateMenuList(graphs);
                int index = InputIndex(menuList, ranges.Length);
                if (index != ranges.Length)
                {
                    var path = stringInput.Input();
                    var range = ranges[index];
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

        private string CreateMenuList(IReadOnlyCollection<GraphEntity> graphs)
        {
            return graphs.Select(k => k.ConvertToString())
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
