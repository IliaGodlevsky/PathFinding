using CommunityToolkit.Mvvm.Messaging;
using LiteDB;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    internal abstract class ImportGraphMenuItem<TPath, TImport> : IMenuItem
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<TPath> input;
        protected readonly ISerializer<IEnumerable<TImport>> serializer;
        protected readonly IService<Vertex> service;
        protected readonly ILog log;

        protected ImportGraphMenuItem(IMessenger messenger,
            IInput<TPath> input,
            ISerializer<IEnumerable<TImport>> serializer,
            ILog log,
            IService<Vertex> service)
        {
            this.service = service;
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
        }

        public virtual async void Execute()
        {
            try
            {
                var path = InputPath();
                var imported = ImportGraph(path).ToList();
                var ids = service.GetAllGraphInfo().Select(x => x.Id).ToReadOnly();
                if (ids.Count == 0 && imported.Count > 0)
                {
                    var graph = AddSingleImported(imported[0]);
                    var costRange = graph.Graph.First().Cost.CostRange;
                    var costMsg = new CostRangeChangedMessage(costRange);
                    messenger.Send(costMsg, Tokens.AppLayout);
                    var graphMsg = new GraphMessage(graph);
                    messenger.SendMany(graphMsg, Tokens.Visual,
                        Tokens.AppLayout, Tokens.Main, Tokens.Common);
                    imported.RemoveAt(0);
                    Post(graph);
                }
                if (imported.Count > 0)
                {
                    await Task.Run(() => AddImported(imported));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected abstract TPath InputPath();

        protected abstract void Post(GraphReadDto<Vertex> dto);

        protected abstract GraphReadDto<Vertex> AddSingleImported(TImport imported);

        protected abstract void AddImported(IEnumerable<TImport> imported);

        protected abstract IReadOnlyCollection<TImport> ImportGraph(TPath path);
    }
}
