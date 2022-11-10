using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Attributes;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class GraphLoadViewModel : ViewModel, IDisposable
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;

        public GraphLoadViewModel(IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, ILog log)
            : base(log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            this.module = module;
        }

        [Order(0)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.LoadGraph)]
        private void LoadGraph()
        {
            var graph = module.LoadGraph();
            messenger.Send(new GraphCreatedMessage(graph));
            messenger.Send(new CostRangeChangedMessage(VertexCost.CostRange));
        }

        [Order(1)]
        [MenuItem(MenuItemsNames.Exit)]
        private void Interrup()
        {
            RaiseViewClosed();
        }

        public override void Dispose()
        {
            base.Dispose();
            messenger.Unregister(this);
        }
    }
}
