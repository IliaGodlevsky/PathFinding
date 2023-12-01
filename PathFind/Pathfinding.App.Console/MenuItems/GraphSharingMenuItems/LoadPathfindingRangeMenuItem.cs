using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using Shared.Extensions;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class LoadPathfindingRangeMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private IGraph<Vertex> graph = Graph<Vertex>.Empty;

        private readonly GraphsPathfindingHistory history;
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly IInput<string> pathInput;
        private readonly ILog log;

        public LoadPathfindingRangeMenuItem(GraphsPathfindingHistory history,
            ISerializer<IEnumerable<ICoordinate>> serializer,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IFilePathInput pathInput,
            ILog log)
        {
            this.history = history;
            this.serializer = serializer;
            this.pathInput = pathInput;
            this.rangeBuilder = rangeBuilder;
            this.log = log;
        }

        public bool CanBeExecuted()
        {
            return history.Count > 0;
        }

        public void Execute()
        {
            try
            {
                string path = pathInput.Input();
                var range = serializer.DeserializeFromFile(path);
                rangeBuilder.Undo();
                rangeBuilder.Include(range, graph);
                var current = history.GetRange(graph.GetHashCode());
                current.Clear();
                current.AddRange(range);
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
