using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Mappers;
using Pathfinding.App.Console.DataAccess.Services;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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
        protected readonly IMapper mapper;

        protected ImportGraphMenuItem(IMessenger messenger,
            IInput<TPath> input,
            IService service,
            IMapper mapper,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ISerializer<IEnumerable<PathfindingHistorySerializationDto>> serializer, ILog log)
        {
            this.service = service;
            this.rangeBuilder = rangeBuilder;
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
            this.mapper = mapper;
        }

        public virtual void Execute()
        {
            try
            {
                var path = InputPath();
                var importedHistory = mapper.Map<PathfindingHistoryCreateDto[]>(ImportGraph(path)).ToReadOnly();
                importedHistory.ForEach(x => service.AddPathfindingHistory(x));
                var ids = service.GetGraphIds().ToList();
                if (ids.Count == importedHistory.Count && ids.Count > 0)
                {
                    int id = ids[0];
                    var graph = service.GetGraph(id);
                    var costRange = graph.First().Cost.CostRange;
                    var costMsg = new CostRangeChangedMessage(costRange);
                    messenger.Send(costMsg, Tokens.AppLayout);
                    var graphMsg = new GraphMessage(graph, id);
                    messenger.SendMany(graphMsg, Tokens.Visual,
                        Tokens.AppLayout, Tokens.Main, Tokens.Common);
                    var range = service.GetRange(id);
                    rangeBuilder.Undo();
                    rangeBuilder.Include(range, graph);
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
