using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Attributes;
using System;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [Order(7)]
    internal sealed class SaveGraphMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;
        private readonly ILog log;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public SaveGraphMenuItem(IMessenger messenger, IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, ILog log)
        {
            this.messenger = messenger;
            this.module = module;
            this.log = log;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            try
            {
                using (Cursor.UseCurrentPositionWithClean())
                {
                    module.SaveGraph(graph);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        public override string ToString()
        {
            return Languages.SaveGraph;
        }
    }
}
