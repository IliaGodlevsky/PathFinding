using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.Domain.Interface;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Export
{
    [LowPriority]
    internal sealed class SavePathfindingRangeMenuItem : IConditionedMenuItem
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;
        private readonly IRequestService<Vertex> service;
        private readonly IInput<string> stringInput;
        private readonly IInput<int> intInput;
        private readonly ILog log;

        public SavePathfindingRangeMenuItem(ISerializer<IEnumerable<ICoordinate>> serializer,
            IRequestService<Vertex> service,
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
            var ids = service.ReadGraphIdsAsync().Result.GraphIds;
            foreach (var id in ids)
            {
                var range = service.ReadRangeAsync(id).Result.Range;
                if (range.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            try
            {
                var ids = (await service.ReadGraphIdsAsync(token)).GraphIds;
                if (ids.Count == 1)
                {
                    var path = stringInput.Input();
                    var range = await service.ReadRangeAsync(ids.First(), token);
                    await serializer.SerializeToFileAsync(range.Range, path, token);
                    return;
                }
                var graphsWithPath = await ids.ToAsyncEnumerable()
                    .WhereAwait(async id => (await service.ReadRangeAsync(id, token)).Range.Count > 0)
                    .ToArrayAsync(token);
                var ranges = await ids.ToAsyncEnumerable()
                    .SelectAwait(async id => await service.ReadRangeAsync(id, token))
                    .ToArrayAsync(token);
                var graphs = (await service.ReadAllGraphInfoAsync())
                    .GraphInformations
                    .Where(x => ids.Contains(x.Id))
                    .ToArray();
                string menuList = CreateMenuList(graphs);
                int index = InputIndex(menuList, ranges.Length);
                if (index != ranges.Length)
                {
                    var path = stringInput.Input();
                    var range = ranges[index];
                    await serializer.SerializeToFileAsync(range.Range, path, token);
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

        private string CreateMenuList(IReadOnlyCollection<GraphInformationModel> graphs)
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
