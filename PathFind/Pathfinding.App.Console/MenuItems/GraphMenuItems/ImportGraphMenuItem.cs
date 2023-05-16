using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal abstract class ImportGraphMenuItem<TPath> : IMenuItem
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<TPath> input;
        protected readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        protected readonly ILog log;

        protected ImportGraphMenuItem(IMessenger messenger, IInput<TPath> input,
            IGraphSerializer<Graph2D<Vertex>, Vertex> serializer, ILog log)
        {
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
        }

        public virtual void Execute()
        {
            try
            {
                var path = InputPath();
                var graph = ImportGraph(path);
                var range = graph.First().Cost.CostRange;
                messenger.SendData(range, Tokens.AppLayout);
                messenger.SendData(graph, Tokens.AppLayout, Tokens.Main, Tokens.Common);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected abstract TPath InputPath();

        protected abstract Graph2D<Vertex> ImportGraph(TPath path);
    }
}
