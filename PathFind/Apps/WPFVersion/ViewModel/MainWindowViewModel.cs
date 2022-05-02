using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Realizations.Graphs;
using System;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure.EventArguments;
using WPFVersion.Infrastructure.EventHandlers;
using WPFVersion.Messages.DataMessages;

namespace WPFVersion.ViewModel
{
    public class MainWindowViewModel : IDisposable
    {
        internal event GraphCreatedEventHandler GraphCreated;

        private readonly IMessenger messenger;

        public MainWindowViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, SetGraph);
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            GraphCreated?.Invoke(this, new GraphCreatedEventArgs((Graph2D)message.Graph));
        }
    }
}