﻿using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    internal abstract class ExportGraphMenuItem<TPath> : IConditionedMenuItem
    {
        protected readonly IInput<TPath> input;
        protected readonly IInput<int> intInput;
        protected readonly ISerializer<GraphsPathfindingHistory> graphSerializer;
        protected readonly GraphsPathfindingHistory history;
        protected readonly ILog log;

        protected ExportGraphMenuItem(IInput<TPath> input,
            IInput<int> intInput,
            GraphsPathfindingHistory history,
            ISerializer<GraphsPathfindingHistory> graphSerializer,
            ILog log)
        {
            this.input = input;
            this.intInput = intInput;
            this.graphSerializer = graphSerializer;
            this.log = log;
            this.history = history;
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
                var toExport = new GraphsPathfindingHistory();
                string menu = CreateMenuList(keys);
                string menuList = string.Concat(menu, "\n", Languages.ChooseGraph + ": ");
                int index = InputIndex(menuList, keys.Count);
                var ids = new List<int>();
                while (true)
                {
                    if (index == keys.Count + 1)
                    {
                        break;
                    }
                    if (index == keys.Count)
                    {
                        ids.AddRange(history.Ids);
                        break;
                    }
                    int key = keys[index].GetHashCode();
                    keys.RemoveAt(index);
                    ids.Add(key);
                    if (keys.Count > 0)
                    {
                        menu = CreateMenuList(keys);
                        menuList = string.Concat(menu, "\n", Languages.ChooseGraph + ": ");
                        index = InputIndex(menuList, keys.Count);
                        break;
                    }
                }
                
                if (ids.Count > 0)
                {
                    var path = input.Input();
                    toExport.Merge(history, ids);
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

        private string CreateMenuList(IReadOnlyCollection<IGraph<Vertex>> graphs)
        {
            return graphs.Select(k => k.ToString())
                .Append(Languages.All)
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
        }

        protected abstract Task ExportAsync(GraphsPathfindingHistory graph, TPath path);
    }
}
