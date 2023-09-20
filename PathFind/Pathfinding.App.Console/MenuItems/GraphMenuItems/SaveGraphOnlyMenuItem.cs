using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class SaveGraphOnlyMenuItem : IConditionedMenuItem
    {
        private readonly IInput<string> stringInput;
        private readonly IInput<int> intInput;
        private readonly GraphsPathfindingHistory history;
        private readonly ISerializer<Graph2D<Vertex>> serializer;
        private readonly ILog log;

        public SaveGraphOnlyMenuItem(IFilePathInput input, 
            IInput<int> intInput, 
            GraphsPathfindingHistory history,
            ISerializer<Graph2D<Vertex>> serializer, 
            ILog log)
        {
            this.stringInput = input;
            this.intInput = intInput;
            this.history = history;
            this.serializer = serializer;
            this.log = log;
        }

        public bool CanBeExecuted()
        {
            return history.Count > 0;
        }

        public async void Execute()
        {
            try
            {
                if (history.Count == 1)
                {
                    var path = stringInput.Input();
                    var graph = history.Graphs.First();
                    await serializer.SerializeToFileAsync(graph, path);
                    return;
                }
                var graphs = history.Graphs.ToList();
                string menuList = CreateMenuList(graphs);
                int index = InputIndex(menuList, graphs.Count);
                if (index != graphs.Count)
                {
                    var path = stringInput.Input();
                    await serializer.SerializeToFileAsync(graphs[index], path);
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

        private string CreateMenuList(IReadOnlyCollection<Graph2D<Vertex>> graphs)
        {
            return graphs.Select(k => k.ToString())
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
        }

        public override string ToString()
        {
            return "Save only graph";
        }
    }
}
