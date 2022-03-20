using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Messages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    internal sealed class StretchFieldAlongAxisViewModel : IDisposable
    {
        private readonly IMessenger messenger;

        private GraphField3D graphField;

        public string Title { get; set; }

        public Axis Axis { get; set; }

        public StretchFieldAlongAxisViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphFieldCreatedMessage>(this, MessageTokens.StretchAlongAxisModel, OnGraphFieldCreated);
        }

        public void StretchAlongAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (graphField != null)
            {
                graphField.StretchAlongAxis(Axis, e.NewValue);
            }
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