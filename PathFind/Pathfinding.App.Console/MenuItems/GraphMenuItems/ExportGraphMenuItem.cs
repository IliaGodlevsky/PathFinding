using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Serialization;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal abstract class ExportGraphMenuItem<TPath>
        : IConditionedMenuItem, ICanRecieveMessage
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<TPath> input;
        protected readonly ISerializer<SerializationInfo> graphSerializer;
        protected readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        protected readonly IPathfindingHistory history;
        protected readonly ILog log;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        protected ExportGraphMenuItem(IMessenger messenger, 
            IInput<TPath> input, 
            IPathfindingHistory history,
            ISerializer<SerializationInfo> graphSerializer,
            IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            ILog log)
        {
            this.messenger = messenger;
            this.input = input;
            this.graphSerializer = graphSerializer;
            this.log = log;
            this.history = history;
            this.rangeBuilder = rangeBuilder;
        }

        public virtual bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public virtual async void Execute()
        {
            try
            {
                var path = input.Input();
                var info = new SerializationInfo
                {
                    Graph = graph,
                    Range = rangeBuilder.Range.GetCoordinates(),
                    History = history
                };
                await ExportAsync(info, path);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public virtual void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
        }

        protected abstract Task ExportAsync(SerializationInfo graph, TPath path);

        private void OnGraphCreated(Graph2D<Vertex> graph)
        {
            this.graph = graph;
        }
    }
}
