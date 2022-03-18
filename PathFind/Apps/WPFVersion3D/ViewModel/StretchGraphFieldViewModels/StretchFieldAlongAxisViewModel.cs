using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using WPFVersion3D.Axes;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Messages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel.StretchGraphFieldViewModels
{
    internal abstract class StretchFieldAlongAxisViewModel : IDisposable
    {
        private readonly IAxis axis;
        protected readonly IMessenger messenger;

        public abstract string Title { get; }

        protected abstract double[] AdditionalOffset { get; }

        protected GraphField3D GraphField { get; private set; }

        public StretchFieldAlongAxisViewModel(IAxis axis, MessageTokens token)
        {
            this.axis = axis;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphFieldCreatedMessage>(this, token, OnGraphFieldCreated);
        }

        public virtual void StretchAlongAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GraphField != null)
            {
                GraphField.StretchAlongAxis(axis, e.NewValue, AdditionalOffset);
            }
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        protected void OnGraphFieldCreated(GraphFieldCreatedMessage message)
        {
            GraphField = message.Value;
        }
    }
}