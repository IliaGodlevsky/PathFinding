using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class GraphLoadViewModel : IViewModel, IDisposable
    {
        public event Action ViewClosed;

        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;

        public GraphLoadViewModel(IGraphSerializationModule<Graph2D<Vertex>, Vertex> module)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            this.module = module;
        }

        [MenuItem(MenuItemsNames.LoadGraph, 0)]
        private void LoadGraph()
        {
            var graph = module.LoadGraph();
            messenger.Send(new GraphCreatedMessage(graph));
            messenger.Send(new CostRangeChangedMessage(VertexCost.CostRange));
        }

        [MenuItem(MenuItemsNames.Exit, 1)]
        private void Interrup()
        {
            ViewClosed?.Invoke();
        }

        public void Dispose()
        {
            ViewClosed = null;
            messenger.Unregister(this);
        }
    }
}
