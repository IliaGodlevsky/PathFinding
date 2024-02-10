using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class LoadPathfindingRangeMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly IInput<string> pathInput;
        private readonly IService service;
        private readonly ILog log;

        private GraphReadDto graph = GraphReadDto.Empty;

        public LoadPathfindingRangeMenuItem(ISerializer<IEnumerable<ICoordinate>> serializer,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IService service,
            IFilePathInput pathInput,
            ILog log)
        {
            this.service = service;
            this.serializer = serializer;
            this.pathInput = pathInput;
            this.rangeBuilder = rangeBuilder;
            this.log = log;
        }

        public bool CanBeExecuted()
        {
            return graph != GraphReadDto.Empty;
        }

        public void Execute()
        {
            try
            {
                string path = pathInput.Input();
                var range = serializer.DeserializeFromFile(path).ToList();
                var vertices = range
                    .Select((x, i) => (Order: i, Id: graph.Graph.Get(x)))
                    .ToArray();
                service.AddRange(vertices, graph.Id);
                rangeBuilder.Undo();
                rangeBuilder.Include(range, graph.Graph);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        public override string ToString()
        {
            return Languages.LoadRange;
        }
    }
}
