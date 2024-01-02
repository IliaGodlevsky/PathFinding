using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
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
        protected readonly ISerializer<IEnumerable<PathfindingHistorySerializationDto>> graphSerializer;
        protected readonly IService service;
        protected readonly ILog log;

        protected ExportGraphMenuItem(IInput<TPath> input,
            IInput<int> intInput,
            IService service,
            ISerializer<IEnumerable<PathfindingHistorySerializationDto>> graphSerializer,
            ILog log)
        {
            this.input = input;
            this.intInput = intInput;
            this.graphSerializer = graphSerializer;
            this.log = log;
            this.service = service;
        }

        public virtual bool CanBeExecuted() => service.GetGraphIds().Count > 0;

        public virtual async void Execute()
        {
            try
            {
                var keys = service.GetAllGraphInfo().ToList();
                if (keys.Count == 1)
                {
                    var path = input.Input();
                    int id = keys[0].Id;
                    var history = service.GetSerializationHistory(id);
                    await ExportAsync(path, history);
                    return;
                }
                var toExport = new List<PathfindingHistorySerializationDto>();
                string menu = CreateMenuList(keys);
                string menuList = string.Concat(menu, "\n", Languages.MenuOptionChoiceMsg);
                int index = InputIndex(menuList, keys.Count);
                while (true)
                {
                    if (index == keys.Count + 1)
                    {
                        break;
                    }
                    if (index == keys.Count)
                    {
                        var toSave = keys
                            .Select(x => service.GetSerializationHistory(x.Id))
                            .ToReadOnly();
                        toExport.AddRange(toSave);
                        break;
                    }
                    int id = keys[index].Id;
                    keys.RemoveAt(index);
                    var history = service.GetSerializationHistory(id);
                    toExport.Add(history);
                    if (keys.Count > 0)
                    {
                        menu = CreateMenuList(keys);
                        menuList = string.Concat(menu, "\n", Languages.MenuOptionChoiceMsg);
                        index = InputIndex(menuList, keys.Count);
                    }
                }

                if (keys.Count > 0)
                {
                    var path = input.Input();
                    await ExportAsync(path, toExport.ToArray());
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

        private string CreateMenuList(IReadOnlyCollection<GraphEntity> graphs)
        {
            return graphs.Select(k => k.ConvertToString())
                .Append(Languages.All)
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
        }

        protected abstract Task ExportAsync(TPath path, params PathfindingHistorySerializationDto[] histories);
    }
}
