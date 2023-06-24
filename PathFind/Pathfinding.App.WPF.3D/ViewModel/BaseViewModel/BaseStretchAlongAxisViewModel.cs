using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.Model;
using System;
using System.Windows;

namespace Pathfinding.App.WPF._3D.ViewModel.BaseViewModel
{
    internal abstract class BaseStretchAlongAxisViewModel : IDisposable
    {
        private readonly IMessenger messenger;

        protected GraphField3D graphField = GraphField3D.Empty;

        protected abstract IAxis Axis { get; }

        public string Title { get; set; }

        protected BaseStretchAlongAxisViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphFieldCreatedMessage>(this, OnGraphFieldCreated);
        }

        public void StretchAlongAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Axis.LocateVertices(e.NewValue);
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        private void OnGraphFieldCreated(GraphFieldCreatedMessage message)
        {
            graphField = message.Value;
        }
    }
}
