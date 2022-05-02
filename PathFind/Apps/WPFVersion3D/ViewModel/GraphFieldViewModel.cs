using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Windows.Media.Media3D;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Messages.ActionMessages;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.ViewModel.BaseViewModel;

namespace WPFVersion3D.ViewModel
{
    internal class GraphFieldViewModel : NotifyPropertyChanged
    {
        private readonly IMessenger messenger;

        private Graph3D graph;
        private IGraphField field;
        private string graphParametres;
        private Point3D fieldPosition;

        public Point3D FieldPosition { get => fieldPosition; set => Set(ref fieldPosition, value); }

        public string GraphParametres { get => graphParametres; set => Set(ref graphParametres, value); }

        public IGraphField GraphField { get => field; set => Set(ref field, value); }

        public GraphFieldViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            FieldPosition = new Point3D(250, 250, 250);
            messenger.Register<GraphFieldCreatedMessage>(this, OnGraphFieldCreated);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<GraphChangedMessage>(this, OnGraphUpdated);
        }

        private void OnGraphFieldCreated(GraphFieldCreatedMessage message)
        {
            GraphField = message.Value;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = (Graph3D)message.Value;
            var graphFieldFactry = DI.Container.Resolve<IGraphFieldFactory>();
            GraphField = graphFieldFactry.CreateGraphField(graph);
            messenger.Send(new GraphFieldCreatedMessage(GraphField));
            GraphParametres = graph.ToString();
        }

        private void OnGraphUpdated(GraphChangedMessage message)
        {
            GraphParametres = graph.ToString();
        }
    }
}