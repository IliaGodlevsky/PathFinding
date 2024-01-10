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
                var toExport = GetHistoriesToSave();
                if (toExport.Count > 0)
                {
                    var path = input.Input();
                    await ExportAsync(path, toExport);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected abstract Task ExportAsync(TPath path,
            IEnumerable<PathfindingHistorySerializationDto> histories);

        private string CreateMenuList(IReadOnlyCollection<GraphEntity> graphs)
        {
            var menu = graphs.Select(k => k.ConvertToString())
                .Append(Languages.All)
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
            return string.Concat(menu, "\n", Languages.MenuOptionChoiceMsg);
        }

        private int InputIndex(string message, int count)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = intInput.Input(message, count + 2, 1) - 1;
                return index;
            }
        }

        private IReadOnlyCollection<PathfindingHistorySerializationDto> GetHistoriesToSave()
        {
            var toExport = new List<PathfindingHistorySerializationDto>();
            var keys = service.GetAllGraphInfo().ToList();
            int index = 1;
            do
            {
                if (index == keys.Count)
                {
                    var histories = keys
                        .Select(x => service.GetSerializationHistory(x.Id))
                        .ToReadOnly();
                    toExport.AddRange(histories);
                    break;
                }
                string menuList = CreateMenuList(keys);
                index = InputIndex(menuList, keys.Count);
                if (index < keys.Count)
                {
                    int id = keys[index].Id;
                    var history = service.GetSerializationHistory(id);
                    toExport.Add(history);
                    keys.RemoveAt(index);
                }
                index = 0;
            } while (index != keys.Count + 1 && keys.Count > 0);
            return toExport.AsReadOnly();
        }
    }
}
