using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class SaveGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IFilePathInput input;
        private readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        private readonly ILog log;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public SaveGraphMenuItem(IMessenger messenger,
            IFilePathInput input,
            IGraphSerializer<Graph2D<Vertex>, Vertex> serializer,
            ILog log)
        {
            this.messenger = messenger;
            this.input = input;
            this.serializer = serializer;
            this.log = log;
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public async void Execute()
        {
            try
            {
                string path = input.Input();
                await serializer.SerializeToFileAsync(graph, path);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void OnGraphCreated(DataMessage<Graph2D<Vertex>> msg)
        {
            graph = msg.Value;
        }

        public override string ToString()
        {
            return Languages.SaveGraph;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
        }
    }
}
