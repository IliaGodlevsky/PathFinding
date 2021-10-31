using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphViewModel;
using System.Threading.Tasks;
using System.Windows.Media;
using WPFVersion.Messages;

namespace WPFVersion.Model
{
    internal sealed class VertexVisualizationModel : VisualizationModel<Brush>
    {
        public VertexVisualizationModel(IGraph graph) : base(graph)
        {
            Messenger.Default.Register<PathFoundMessage>(this, MessageTokens.VisualizationModel, PathFound);
            Messenger.Default.Register<AlgorithmChosenMessage>(this, MessageTokens.VisualizationModel, AlgorithmChosen);
            Messenger.Default.Register<ShowAlgorithmVisualization>(this, MessageTokens.VisualizationModel, ShowVisualization);
        }

        public override void ShowAlgorithmVisualization(IAlgorithm algorithm)
        {
            Task.Run(() =>
            {
                HideVisualiztion();
                base.ShowAlgorithmVisualization(algorithm);
            });
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

        public override void HideVisualiztion()
        {
            graph.Refresh();
        }
    }
}
