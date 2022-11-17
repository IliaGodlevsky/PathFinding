using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    internal sealed class GraphLoadViewModel : SafeViewModel, IDisposable
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;

        public GraphLoadViewModel(IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, ILog log)
            : base(log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            this.module = module;
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.LoadGraph, 0)]
        private void LoadGraph()
        {
            var graph = module.LoadGraph();
            messenger.Send(new GraphCreatedMessage(graph));
            messenger.Send(new CostRangeChangedMessage(VertexCost.CostRange));
        }

        public override void Dispose()
        {
            base.Dispose();
            messenger.Unregister(this);
        }
    }
}
