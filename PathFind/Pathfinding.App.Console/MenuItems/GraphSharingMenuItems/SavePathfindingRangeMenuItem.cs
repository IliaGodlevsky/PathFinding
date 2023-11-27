using Pathfinding.App.Console.DataAccess;
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
        private readonly GraphsPathfindingHistory history;
        private readonly IInput<string> stringInput;
        private readonly IInput<int> intInput;
        private readonly ILog log;

        public SavePathfindingRangeMenuItem(ISerializer<IEnumerable<ICoordinate>> serializer,
            GraphsPathfindingHistory history,
            IFilePathInput stringInput,
            IInput<int> intInput,
            ILog log)
        {
            this.serializer = serializer;
            this.log = log;
            this.history = history;
            this.stringInput = stringInput;
            this.intInput = intInput;
        }

        public bool CanBeExecuted()
        {
            return history.Ids
                .Select(id => history.GetRange(id))
                .Any(range => range.Count > 0);
        }

        public async void Execute()
        {
            try
            {
                if (history.Count == 1)
                {
                    var path = stringInput.Input();
                    int id = history.Ids.First();
                    var range = history.GetRange(id);
                    await serializer.SerializeToFileAsync(range, path);
                    return;
                }
                var ids = history.Ids
                    .Where(id => history.GetRange(id).Count > 0)
                    .ToArray();
                var ranges = ids
                    .Select(id => history.GetRange(id))
                    .ToArray();
                var graphs = ids
                    .Select(id => history.GetGraph(id))
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

        private string CreateMenuList(IReadOnlyCollection<IGraph<Vertex>> graphs)
        {
            return graphs.Select(k => k.ToString())
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
