using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal abstract class ExportGraphMenuItem<TPath> : IConditionedMenuItem
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<TPath> input;
        protected readonly IInput<int> intInput;
        protected readonly ISerializer<PathfindingHistory> graphSerializer;
        protected readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        protected readonly PathfindingHistory history;
        protected readonly ILog log;

        protected ExportGraphMenuItem(IMessenger messenger,
            IInput<TPath> input,
            IInput<int> intInput,
            PathfindingHistory history,
            ISerializer<PathfindingHistory> graphSerializer,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ILog log)
        {
            this.messenger = messenger;
            this.input = input;
            this.intInput = intInput;
            this.graphSerializer = graphSerializer;
            this.log = log;
            this.history = history;
            this.rangeBuilder = rangeBuilder;
        }

        public virtual bool CanBeExecuted() => history.Count > 0;

        public virtual async void Execute()
        {
            try
            {
                if (history.Count == 1)
                {
                    var path = input.Input();
                    await ExportAsync(history, path);
                    return;
                }
                var keys = history.Graphs.ToList();
                var toExport = new PathfindingHistory();
                string menuList = CreateMenuList(keys);
                int index = InputIndex(menuList, keys.Count);
                while (index != keys.Count + 1)
                {
                    if (index == keys.Count)
                    {
                        toExport = history;
                        break;
                    }
                    var key = keys[index];
                    var toAdd = history.GetFor(key);
                    keys.Remove(key);
                    toExport.Add(key, toAdd);
                    if (keys.Count == 0)
                    {
                        break;
                    }
                    menuList = CreateMenuList(keys);
                    index = InputIndex(menuList, keys.Count);
                }
                if (toExport.Count > 0)
                {
                    var path = input.Input();
                    await ExportAsync(toExport, path);
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
                int index = intInput.Input(message, count + 2, 1) - 1;
                return index;
            }
        }

        private string CreateMenuList(IReadOnlyCollection<Graph2D<Vertex>> graphs)
        {
            return graphs.Select(k => k.ToString())
                .Append(Languages.All)
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
        }

        protected abstract Task ExportAsync(PathfindingHistory graph, TPath path);
    }
}
