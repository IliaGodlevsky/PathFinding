using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Messages;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Serialization;
using System;

namespace ConsoleVersion.ViewModel
{
    internal sealed class GraphLoadViewModel : IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;
        private readonly GraphSerializationModule module;

        public GraphLoadViewModel(GraphSerializationModule module)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            this.module = module;
        }

        [MenuItem(MenuItemsNames.LoadGraph, 0)]
        private void LoadGraph()
        {
            var graph = module.LoadGraph();
            messenger.Send(new GraphCreatedMessage(graph));
            messenger.Send(new CostRangeChangedMessage(BaseVertexCost.CostRange));
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
