using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
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
        protected readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        protected readonly ILog log;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        protected ExportGraphMenuItem(IMessenger messenger, IInput<TPath> input,
            IGraphSerializer<Graph2D<Vertex>, Vertex> serializer, ILog log)
        {
            this.messenger = messenger;
            this.input = input;
            this.serializer = serializer;
            this.log = log;
        }

        public virtual bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public virtual async void Execute()
        {
            try
            {
                var path = input.Input();
                await ExportAsync(path);
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

        protected abstract Task ExportAsync(TPath path);

        private void OnGraphCreated(DataMessage<Graph2D<Vertex>> msg)
        {
            graph = msg.Value;
        }
    }
}
