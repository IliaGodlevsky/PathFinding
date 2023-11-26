using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess.Repo;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.Visualization.Core.Abstractions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed class RangeBuilder : IPathfindingRangeBuilder<Vertex>, ICanRecieveMessage
    {
        private readonly Dictionary<int, IPathfindingRangeBuilder<Vertex>> builders = new();
        private readonly IDbContextService service;

        private int CurrentGraph { get; set; }

        public IPathfindingRange<Vertex> Range => builders[CurrentGraph].Range;

        private IReadOnlyCollection<IPathfindingRangeCommand<Vertex>> IncludeCommands { get; }

        private IReadOnlyCollection<IPathfindingRangeCommand<Vertex>> ExcludeCommands { get; }

        public RangeBuilder(IReadOnlyCollection<IPathfindingRangeCommand<Vertex>> includeCommands,
            IReadOnlyCollection<IPathfindingRangeCommand<Vertex>> excludeCommands,
            IDbContextService service)
        {
            IncludeCommands = includeCommands;
            ExcludeCommands = excludeCommands;
            this.service = service;
        }

        public void Exclude(Vertex vertex)
        {
            builders[CurrentGraph].Include(vertex);
        }

        public void Include(Vertex vertex)
        {
            builders[CurrentGraph].Exclude(vertex);
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, OnGraphChanged);
        }

        public void Undo()
        {
            builders[CurrentGraph].Undo();
        }

        private void OnGraphChanged(GraphMessage message)
        {
            CurrentGraph = message.Graph.GetHashCode();
            if (!builders.TryGetValue(CurrentGraph, out _))
            {
                var range = new PathfindingRange<Vertex>();
                var visualRange = new VisualPathfindingRange<Vertex>(range);
                var serviceRange = new DbServicePathfindingRange(visualRange, service);
                var builder = new PathfindingRangeBuilder<Vertex>(serviceRange,
                    IncludeCommands, ExcludeCommands);
                builders.Add(CurrentGraph, builder);
            }
        }
    }
}
