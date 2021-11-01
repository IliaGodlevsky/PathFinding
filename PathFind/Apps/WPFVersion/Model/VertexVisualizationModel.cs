using Algorithm.Infrastructure.EventArguments;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using GraphViewModel;
using System;
using System.Threading.Tasks;
using WPFVersion.Messages;

namespace WPFVersion.Model
{
    internal sealed class VertexVisualizationModel : VisualizationModel, IDisposable
    {
        public VertexVisualizationModel(IGraph graph) : base(graph)
        {
            Messenger.Default.Register<PathFoundMessage>(this, MessageTokens.VisualizationModel, PathFound);
            Messenger.Default.Register<AlgorithmChosenMessage>(this, MessageTokens.VisualizationModel, AlgorithmChosen);
            Messenger.Default.Register<ShowAlgorithmVisualization>(this, MessageTokens.VisualizationModel, ShowVisualization);
            Messenger.Default.Register<RemoveVisualizationMessage>(this, MessageTokens.VisualizationModel, RemoveVisualization);
        }

        public override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e));
        }

        public override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexVisited(sender, e));
        }

        private void AlgorithmChosen(AlgorithmChosenMessage message)
        {
            SubscribeOnAlgorithmEvents(message.Algorithm);
            AddEndPoints(message.Algorithm, message.EndPoints);
        }

        private void ShowVisualization(ShowAlgorithmVisualization message)
        {
            ShowAlgorithmVisualization(message.Algorithm);
        }

        private void PathFound(PathFoundMessage message)
        {
            AddPathVertices(message.Algorithm, message.Path);
        }

        private void RemoveVisualization(RemoveVisualizationMessage message)
        {
            Remove(message.Algorithm);
        }

        public void Dispose()
        {
            Messenger.Default.Unregister(this);
        }
    }
}
