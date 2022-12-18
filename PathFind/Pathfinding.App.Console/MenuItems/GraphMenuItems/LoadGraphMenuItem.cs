using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class LoadGraphMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;
        private readonly ILog log;

        public int Order => 6;

        public LoadGraphMenuItem(IMessenger messenger, 
            IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, ILog log)
        {
            this.messenger = messenger;
            this.module = module;
            this.log = log;
        }

        public bool CanBeExecuted() => true;

        public void Execute()
        {
            try
            {
                using (Cursor.UseCurrentPositionWithClean())
                {
                    var graph = module.LoadGraph();
                    var range = graph.First().Cost.CostRange;
                    messenger.Send(new CostRangeChangedMessage(range));                   
                    messenger.Send(new GraphCreatedMessage(graph), MessageTokens.Screen);
                    messenger.Send(new GraphCreatedMessage(graph), MessageTokens.MainViewModel);
                    messenger.Send(new GraphCreatedMessage(graph));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString()
        {
            return Languages.LoadGraph;
        }
    }
}
