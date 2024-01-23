using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    internal abstract class ImportGraphMenuItem<TPath> : IMenuItem
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<TPath> input;
        protected readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        protected readonly ISerializer<IEnumerable<PathfindingHistorySerializationDto>> serializer;
        protected readonly IService service;
        protected readonly ILog log;

        protected ImportGraphMenuItem(IMessenger messenger,
            IInput<TPath> input,
            IService service,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ISerializer<IEnumerable<PathfindingHistorySerializationDto>> serializer, ILog log)
        {
            this.service = service;
            this.rangeBuilder = rangeBuilder;
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
                    var toAdd = new[] { imported[0] };
                    var history = service.AddPathfindingHistory(toAdd).First();
                    var graph = history.Graph;
                    var costRange = graph.First().Cost.CostRange;
                    var costMsg = new CostRangeChangedMessage(costRange);
                    messenger.Send(costMsg, Tokens.AppLayout);
                    var graphMsg = new GraphMessage(graph, history.Id);
                    messenger.SendMany(graphMsg, Tokens.Visual,
                        Tokens.AppLayout, Tokens.Main, Tokens.Common);
                    var range = history.Range;
                    rangeBuilder.Undo();
                    rangeBuilder.Include(range, graph);
                    imported.RemoveAt(0);
                }
                if (imported.Count > 0)
                {
                    await Task.Run(() => service.AddPathfindingHistory(imported));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected abstract TPath InputPath();

        protected abstract IReadOnlyCollection<PathfindingHistorySerializationDto> ImportGraph(TPath path);
    }
}
