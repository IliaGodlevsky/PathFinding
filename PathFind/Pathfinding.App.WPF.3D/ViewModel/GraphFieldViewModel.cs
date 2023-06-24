using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Messages.ActionMessages;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.App.WPF._3D.ViewModel.BaseViewModel;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.ViewModel
{
    using FieldFactory = IGraphFieldFactory<Graph3D<Vertex3D>, Vertex3D, GraphField3D>;

    internal class GraphFieldViewModel : NotifyPropertyChanged
    {
        private readonly IMessenger messenger;

        private Graph3D<Vertex3D> graph = Graph3D<Vertex3D>.Empty;
        private GraphField3D field;
        private string graphParametres;
        private Point3D fieldPosition;

        public Point3D FieldPosition
        {
            get => fieldPosition;
            set => Set(ref fieldPosition, value);
        }

        public string GraphParametres
        {
            get => graphParametres;
            set => Set(ref graphParametres, value);
        }

        public GraphField3D GraphField
        {
            get => field;
            set => Set(ref field, value);
        }

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
            graph = message.Value;
            var factory = DI.Container.Resolve<FieldFactory>();
            GraphField = factory.CreateGraphField(graph);
            messenger.Send(new GraphFieldCreatedMessage(GraphField));
            GraphParametres = graph.GetStringRepresentation();
        }

        private void OnGraphUpdated(GraphChangedMessage message)
        {
            GraphParametres = graph.GetStringRepresentation();
        }
    }
}