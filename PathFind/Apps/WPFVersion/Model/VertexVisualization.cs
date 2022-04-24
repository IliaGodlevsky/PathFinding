using Common.Extensions;
using GraphLib.Interfaces;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using WPFVersion.Extensions;

namespace WPFVersion.Model
{
    internal sealed class VertexVisualization : IVisualization<Vertex>
    {
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

        private static Dispatcher Dispatcher => Application.Current.Dispatcher;

        public bool IsVisualizedAsPath(Vertex vertex)
        {
            return Dispatcher.Invoke(() =>
            {
                return vertex.VertexColor.IsOneOf(AlreadyPathVertexColor, PathVertexColor);
            });
        }

        public bool IsVisualizedAsEndPoint(Vertex vertex)
        {
            return Dispatcher.Invoke(() =>
            {
                return vertex.VertexColor.IsOneOf(SourceVertexColor, TargetVertexColor, IntermediateVertexColor, ToReplaceMarkColor);
            });
        }

        public bool IsVisualizedAsEnqueued(Vertex vertex)
        {
            return Dispatcher.Invoke(() =>
            {
                return vertex.VertexColor == EnqueuedVertexColor;
            });
        }

        public void VisualizeAsTarget(Vertex vertex)
        {
            Dispatcher.Invoke(() => vertex.VertexColor = TargetVertexColor);
        }

        public void VisualizeAsIntermediate(Vertex vertex)
        {
            Dispatcher.Invoke(() => vertex.VertexColor = IntermediateVertexColor);
        }

        public void VisualizeAsObstacle(Vertex vertex)
        {
            Dispatcher.Invoke(() => vertex.VertexColor = ObstacleVertexColor);
        }

        public void VisualizeAsSource(Vertex vertex)
        {
            Dispatcher.Invoke(() => vertex.VertexColor = SourceVertexColor);
        }

        public void VisualizeAsRegular(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                vertex.VertexColor = RegularVertexColor;
            });
        }

        public void VisualizeAsPath(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (vertex.IsVisualizedAsPath)
                {
                    vertex.VertexColor = AlreadyPathVertexColor;
                }
                else
                {
                    vertex.VertexColor = PathVertexColor;
                }
            });
        }

        public void VisualizeAsVisited(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsVisualizedAsPath)
                {
                    vertex.VertexColor = VisitedColor;
                }
            });
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsVisualizedAsPath)
                {
                    vertex.VertexColor = EnqueuedVertexColor;
                }
            });
        }

        public void VisualizeAsMarkedToReplaceIntermediate(Vertex vertex)
        {
            if (vertex.VertexColor == IntermediateVertexColor)
            {
                Dispatcher.Invoke(() => vertex.VertexColor = ToReplaceMarkColor);
            }
        }
    }
}
