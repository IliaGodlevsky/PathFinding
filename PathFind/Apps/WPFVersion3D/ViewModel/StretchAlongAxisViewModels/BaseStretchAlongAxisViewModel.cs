using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Interface;
using WPFVersion3D.Messages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel.StretchAlongAxisViewModels
{
    internal abstract class BaseStretchAlongAxisViewModel : IDisposable
    {
        private readonly IMessenger messenger;

        protected GraphField3D graphField;

        protected abstract IAxis Axis { get; }

        public string Title { get; set; }

        public BaseStretchAlongAxisViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphFieldCreatedMessage>(this, MessageTokens.StretchAlongAxisModel, OnGraphFieldCreated);
        }

        public void StretchAlongAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Axis?.LocateVertices(e.NewValue);
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
