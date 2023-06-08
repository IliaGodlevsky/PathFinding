using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class LoadRangeMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly ILog log;
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;
        private readonly IFilePathInput fileInput;
        private readonly IPathfindingRangeBuilder<Vertex> builder;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public LoadRangeMenuItem(ILog log, 
            ISerializer<IEnumerable<ICoordinate>> serializer, 
            IFilePathInput fileInput, 
            IPathfindingRangeBuilder<Vertex> builder)
        {
            this.log = log;
            this.serializer = serializer;
            this.fileInput = fileInput;
            this.builder = builder;
        }

        public bool CanBeExecuted()
        {
            return graph != Graph2D<Vertex>.Empty;
        }

        public void Execute()
        {
            try
            {
                string path = fileInput.Input();
                var range = LoadCoordinates(path);
                builder.Undo();
                builder.Include(range, graph);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private IReadOnlyCollection<ICoordinate> LoadCoordinates(string path)
        {
            var range = serializer.DeserializeFromFile(path).ToList();
            var target = range[range.Count - 1];
            range.RemoveAt(range.Count - 1);
            range.Insert(1, target);
            return range.AsReadOnly();
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        public override string ToString()
        {
            return "Load range";
        }
    }
}
