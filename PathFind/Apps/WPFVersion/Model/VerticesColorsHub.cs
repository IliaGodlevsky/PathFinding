using Common.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using WPFVersion.Extensions;

namespace WPFVersion.Model
{
    internal sealed class VerticesColorsHub
    {
        private static Dispatcher Dispatcher => Application.Current.Dispatcher;
        private static readonly Brush VisitedColor = new SolidColorBrush(Colors.CadetBlue);
        private static readonly Brush PathVertexColor = new SolidColorBrush(Colors.Yellow);
        private static readonly Brush SourceVertexColor = new SolidColorBrush(Colors.Green);
        private static readonly Brush TargetVertexColor = new SolidColorBrush(Colors.Red);
        private static readonly Brush EnqueuedVertexColor = new SolidColorBrush(Colors.Magenta);
        private static readonly Brush ObstacleVertexColor = new SolidColorBrush(Colors.Black);
        private static readonly Brush RegularVertexColor = new SolidColorBrush(Colors.White);
        private static readonly Brush AlreadyPathVertexColor = new SolidColorBrush(Colors.Gold);
        private static readonly Brush IntermediateVertexColor = new SolidColorBrush(Colors.DarkOrange);
        private static readonly Brush ToReplaceMarkColor = new SolidColorBrush(Colors.DarkOrange.SetBrightness(72.5));

        public bool IsVisualizedAsPath(Vertex vertex)
        {
            return Dispatcher.Invoke(() =>
            {
                return vertex.Background.IsOneOf(AlreadyPathVertexColor, 
                    PathVertexColor, IntermediateVertexColor, ToReplaceMarkColor);
            });
        }

        public bool IsVisualizedAsEndPoints(Vertex vertex)
        {
            return Dispatcher.Invoke(() =>
            {
                return vertex.Background.IsOneOf(SourceVertexColor, 
                    TargetVertexColor, IntermediateVertexColor, ToReplaceMarkColor);
            });           
        }

        public void VisualizeAsTarget(Vertex vertex) => Dispatcher.Invoke(() => vertex.Background = TargetVertexColor);
        public void VisualizeAsIntermediate(Vertex vertex) => Dispatcher.Invoke(() => vertex.Background = IntermediateVertexColor);
        public void VisualizeAsObstacle(Vertex vertex) => Dispatcher.Invoke(() => vertex.Background = ObstacleVertexColor);
        public void VisualizeAsSource(Vertex vertex) => Dispatcher.Invoke(() => vertex.Background = SourceVertexColor);

        public void VisualizeAsRegular(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsObstacle)
                {
                    vertex.Background = RegularVertexColor;
                }
            });
        }

        public void VisualizeAsPath(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (vertex.IsVisualizedAsPath)
                {
                    vertex.Background = AlreadyPathVertexColor;
                }
                else
                {
                    vertex.Background = PathVertexColor;
                }
            });
        }

        public void VisualizeAsVisited(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsVisualizedAsPath)
                {
                    vertex.Background = VisitedColor;
                }
            });
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsVisualizedAsPath)
                {
                    vertex.Background = EnqueuedVertexColor;
                }
            });
        }

        public void VisualizeAsMarkedToReplaceIntermediate(Vertex vertex)
        {
            if (vertex.Background == IntermediateVertexColor)
            {
                Dispatcher.Invoke(() => vertex.Background = ToReplaceMarkColor);
            }
        }
    }
}
