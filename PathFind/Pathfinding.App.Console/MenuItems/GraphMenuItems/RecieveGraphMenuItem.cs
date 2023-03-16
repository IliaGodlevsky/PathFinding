using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class RecieveGraphMenuItem : IMenuItem
    {
        private readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        private readonly IMessenger messenger;
        private readonly IInput<string> input;
        private readonly ILog log;

        public RecieveGraphMenuItem(IGraphSerializer<Graph2D<Vertex>, Vertex> serializer,
            IMessenger messenger, IInput<string> input, ILog log)
        {
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
        }

        public void Execute()
        {
            try
            {
                var graph = Graph2D<Vertex>.Empty;
                using (Cursor.UseCurrentPositionWithClean())
                {
                    string pipeName = input.Input(Languages.InputPipeMsg);
                    System.Console.Write(Languages.WaitingForConnection);
                    graph = serializer.LoadGraphFromPipe(pipeName);
                }
                var range = graph.First().Cost.CostRange;
                messenger.SendData(range, Tokens.Screen);
                messenger.SendData(graph, Tokens.Screen | Tokens.Main | Tokens.Common);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString()
        {
            return Languages.RecieveGraph;
        }
    }
}
