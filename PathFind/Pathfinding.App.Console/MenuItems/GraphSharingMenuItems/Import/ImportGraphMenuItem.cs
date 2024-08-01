using CommunityToolkit.Mvvm.Messaging;
using LiteDB;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
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

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    internal abstract class ImportGraphMenuItem<TPath, TImport> : IMenuItem
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<TPath> input;
        protected readonly ISerializer<IEnumerable<TImport>> serializer;
        protected readonly IRequestService<Vertex> service;
        protected readonly ILog log;

        protected ImportGraphMenuItem(IMessenger messenger,
            IInput<TPath> input,
            ISerializer<IEnumerable<TImport>> serializer,
            ILog log,
            IRequestService<Vertex> service)
        {
            this.service = service;
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
        }

        public virtual async Task ExecuteAsync(CancellationToken token = default)
        {
            try
            {
                var path = InputPath();
                var imported = (await ImportGraph(path, token)).ToList();
                var ids = (await service.ReadAllGraphInfoAsync(token))
                    .GraphInformations
                    .Select(x => x.Id)
                    .ToReadOnly();
                if (ids.Count == 0 && imported.Count > 0)
                {
                    var graph = await AddSingleImported(imported[0], token);
                    var costRange = graph.Graph.First().Cost.CostRange;
                    var costMsg = new CostRangeChangedMessage(costRange);
                    messenger.Send(costMsg, Tokens.AppLayout);
                    var graphMsg = new GraphMessage(graph);
                    messenger.SendMany(graphMsg, Tokens.Visual,
                        Tokens.AppLayout, Tokens.Main, Tokens.Common);
                    imported.RemoveAt(0);
                    await Post(graph, token);
                }
                if (imported.Count > 0)
                {
                    await AddImported(imported, token);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected abstract TPath InputPath();

        protected abstract Task Post(GraphModel<Vertex> model, CancellationToken token);

        protected abstract Task<GraphModel<Vertex>> AddSingleImported(TImport imported,
            CancellationToken token);

        protected abstract Task AddImported(IEnumerable<TImport> imported,
            CancellationToken token);

        protected abstract Task<IReadOnlyCollection<TImport>> ImportGraph(TPath path,
            CancellationToken token);
    }
}
