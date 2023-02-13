using Pathfinding.App.WPF._2D.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pathfinding.App.WPF._2D.Model
{
    internal class VertexVisualization : IVisualization<Vertex>
    {
        private static readonly SolidColorBrush VisitedColor = new SolidColorBrush(Colors.CadetBlue);
        private static readonly SolidColorBrush PathVertexColor = new SolidColorBrush(Colors.Yellow);
        private static readonly SolidColorBrush SourceVertexColor = new SolidColorBrush(Colors.Green);
        private static readonly SolidColorBrush TargetVertexColor = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush EnqueuedVertexColor = new SolidColorBrush(Colors.Magenta);
        private static readonly SolidColorBrush ObstacleVertexColor = new SolidColorBrush(Colors.Black);
        private static readonly SolidColorBrush RegularVertexColor = new SolidColorBrush(Colors.White);
        private static readonly SolidColorBrush AlreadyPathVertexColor = new SolidColorBrush(Colors.Gold);
        private static readonly SolidColorBrush IntermediateVertexColor = new SolidColorBrush(Colors.DarkOrange);
        private static readonly SolidColorBrush ToReplaceMarkColor = new SolidColorBrush(Colors.DarkOrange.SetBrightness(72.5));

        private static Dispatcher Dispatcher => Application.Current.Dispatcher;

        public bool IsVisualizedAsPath(Vertex vertex)
        {
            return Dispatcher.Invoke(() =>
            {
                return vertex.VertexColor.IsOneOf(AlreadyPathVertexColor, PathVertexColor);
            });
        }

        public bool IsVisualizedAsPathfindingRange(Vertex vertex)
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

        public void VisualizeAsTransit(Vertex vertex)
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
                if (!vertex.IsVisualizedAsPathfindingRange())
                {
                    if (vertex.IsVisualizedAsPath())
                    {
                        vertex.VertexColor = AlreadyPathVertexColor;
                    }
                    else
                    {
                        vertex.VertexColor = PathVertexColor;
                    }
                }
            });
        }

        public void VisualizeAsVisited(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsVisualizedAsPath() && !vertex.IsVisualizedAsPathfindingRange())
                {
                    vertex.VertexColor = VisitedColor;
                }
            });
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsVisualizedAsPath() && !vertex.IsVisualizedAsPathfindingRange())
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
