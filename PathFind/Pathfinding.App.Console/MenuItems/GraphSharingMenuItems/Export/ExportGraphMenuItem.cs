using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Export
{
    internal abstract class ExportGraphMenuItem<TPath, TExport> : IConditionedMenuItem
    {
        protected readonly IInput<TPath> input;
        protected readonly IInput<int> intInput;
        protected readonly ISerializer<IEnumerable<TExport>> serializer;
        protected readonly IRequestService<Vertex> service;
        protected readonly ILog log;

        protected ExportGraphMenuItem(IInput<TPath> input,
            IInput<int> intInput,
            ISerializer<IEnumerable<TExport>> graphSerializer,
            ILog log,
            IRequestService<Vertex> service)
        {
            this.input = input;
            this.intInput = intInput;
            serializer = graphSerializer;
            this.log = log;
            this.service = service;
        }

        public virtual bool CanBeExecuted() => service.ReadGraphCountAsync().Result > 0;

        public virtual async Task ExecuteAsync(CancellationToken token = default)
        {
            try
            {
                var toExport = await GetHistoriesToSave(token);
                if (toExport.Count > 0)
                {
                    var path = input.Input();
                    await ExportAsync(path, toExport, token);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected abstract Task ExportAsync(TPath path,
            IEnumerable<TExport> histories,
            CancellationToken token);

        private string CreateMenuList(IReadOnlyCollection<GraphInformationModel> graphs)
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

        protected abstract Task<TExport> GetForSave(int graphId, CancellationToken token);

        private async Task<IReadOnlyCollection<TExport>> GetHistoriesToSave(CancellationToken token)
        {
            var toExport = new List<TExport>();
            var keys = (await service.ReadAllGraphInfoAsync(token))
                .GraphInformations
                .ToList();
            int index = 1;
            do
            {
                if (index == keys.Count)
                {
                    var histories = await keys.ToAsyncEnumerable()
                        .SelectAwait(async x => await GetForSave(x.Id, token))
                        .ToListAsync(token);
                    toExport.AddRange(histories);
                    break;
                }
                string menuList = CreateMenuList(keys);
                index = InputIndex(menuList, keys.Count);
                if (index < keys.Count)
                {
                    int id = keys[index].Id;
                    var history = await GetForSave(id, token);
                    toExport.Add(history);
                    keys.RemoveAt(index);
                    index = 0;
                }
            } while (index != keys.Count + 1 && keys.Count > 0);
            return toExport.AsReadOnly();
        }
    }
}
