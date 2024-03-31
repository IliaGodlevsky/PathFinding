using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Export
{
    internal abstract class ExportGraphMenuItem<TPath, TExport> : IConditionedMenuItem
    {
        protected readonly IInput<TPath> input;
        protected readonly IInput<int> intInput;
        protected readonly ISerializer<IEnumerable<TExport>> serializer;
        protected readonly IService<Vertex> service;
        protected readonly ILog log;

        protected ExportGraphMenuItem(IInput<TPath> input,
            IInput<int> intInput,
            ISerializer<IEnumerable<TExport>> graphSerializer,
            ILog log,
            IService<Vertex> service)
        {
            this.input = input;
            this.intInput = intInput;
            serializer = graphSerializer;
            this.log = log;
            this.service = service;
        }

        public virtual bool CanBeExecuted() => service.GetGraphCount() > 0;

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
            IEnumerable<TExport> histories);

        private string CreateMenuList(IReadOnlyCollection<GraphInformationReadDto> graphs)
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

        protected abstract TExport GetForSave(int graphId);

        private IReadOnlyCollection<TExport> GetHistoriesToSave()
        {
            var toExport = new List<TExport>();
            var keys = service.GetAllGraphInfo().ToList();
            int index = 1;
            do
            {
                if (index == keys.Count)
                {
                    var histories = keys
                        .Select(x => GetForSave(x.Id))
                        .ToReadOnly();
                    toExport.AddRange(histories);
                    break;
                }
                string menuList = CreateMenuList(keys);
                index = InputIndex(menuList, keys.Count);
                if (index < keys.Count)
                {
                    int id = keys[index].Id;
                    var history = GetForSave(id);
                    toExport.Add(history);
                    keys.RemoveAt(index);
                    index = 0;
                }
            } while (index != keys.Count + 1 && keys.Count > 0);
            return toExport.AsReadOnly();
        }
    }
}
