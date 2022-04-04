using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel
{
    internal class GraphFieldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IMessenger messenger;

        private IGraphField field;
        private AxisAngleRotation3D xAxis;
        private AxisAngleRotation3D yAxis;
        private AxisAngleRotation3D zAxis;

        public AxisAngleRotation3D XAxisAngleRotation 
        {
            get => xAxis;
            set { xAxis = value; OnPropertyChanged(); }
        }

        public AxisAngleRotation3D YAxisAngleRotation
        {
            get => yAxis;
            set { yAxis = value; OnPropertyChanged(); }
        }

        public AxisAngleRotation3D ZAxisAngleRotation
        {
            get => zAxis;
            set { zAxis = value; OnPropertyChanged(); }
        }

        public IGraphField GraphField 
        {
            get => field;
            set { field = value; OnPropertyChanged(); }
        }

        public GraphFieldViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphFieldCreatedMessage>(this, MessageTokens.GraphFieldModel, OnGraphFieldCreated);
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnGraphFieldCreated(GraphFieldCreatedMessage message)
        {
            GraphField = message.Value;
        }
    }
}