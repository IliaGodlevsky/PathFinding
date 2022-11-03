using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization.Interfaces;
using System;

namespace ConsoleVersion.ViewModel
{
    internal sealed class GraphLoadViewModel : IViewModel, IDisposable
    {
        public event Action WindowClosed;

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
            messenger.Send(new CostRangeChangedMessage(graph.CostRange));
        }

        [MenuItem(MenuItemsNames.Exit, 1)]
        private void Interrup()
        {
            WindowClosed?.Invoke();
        }

        public void Dispose()
        {
            WindowClosed = null;
            messenger.Unregister(this);
        }
    }
}
