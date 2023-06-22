using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Serialization;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
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
        protected readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        protected readonly ISerializer<SerializationInfo> serializer;
        protected readonly ILog log;

        protected ImportGraphMenuItem(IMessenger messenger, 
            IInput<TPath> input, 
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ISerializer<SerializationInfo> serializer, 
            ILog log)
        {
            this.rangeBuilder = rangeBuilder;
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
                var info = ImportGraph(path);
                ApplySerializationInfo(info);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void ApplySerializationInfo(SerializationInfo info)
        {
            var costRange = info.Graph.First().Cost.CostRange;
            messenger.SendData(costRange, Tokens.AppLayout);
            messenger.SendData(info.Graph, Tokens.AppLayout, Tokens.Main, Tokens.Common);
            var pathfindingRange = info.Range.ToList();
            var target = pathfindingRange[pathfindingRange.Count - 1];
            pathfindingRange.RemoveAt(pathfindingRange.Count - 1);
            pathfindingRange.Insert(1, target);
            rangeBuilder.Undo();
            rangeBuilder.Include(pathfindingRange, info.Graph);
            messenger.SendData(info, Tokens.Storage);
        }

        protected abstract TPath InputPath();

        protected abstract SerializationInfo ImportGraph(TPath path);
    }
}
