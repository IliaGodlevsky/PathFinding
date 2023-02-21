using Pathfinding.App.WPF._3D.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class VertexVisualization : ITotalVisualization<Vertex3D>
    {
        private static Dispatcher Dispatcher => Application.Current.Dispatcher;

        public static Brush VisitedVertexBrush = new SolidColorBrush(Colors.CadetBlue) { Opacity = Constants.InitialVisitedVertexOpacity };
        public static Brush ObstacleVertexBrush = new SolidColorBrush(Colors.Black) { Opacity = Constants.InitialObstacleVertexOpacity };
        public static Brush RegularVertexBrush = new SolidColorBrush(Colors.White) { Opacity = Constants.InitialRegularVertexOpacity };
        public static Brush PathVertexBrush = new SolidColorBrush(Colors.Yellow) { Opacity = Constants.InitialPathVertexOpacity };
        public static Brush SourceVertexBrush = new SolidColorBrush(Colors.Green) { Opacity = Constants.InitialSourceVertexOpacity };
        public static Brush TargetVertexBrush = new SolidColorBrush(Colors.Red) { Opacity = Constants.InitialTargetVertexOpacity };
        public static Brush EnqueuedVertexBrush = new SolidColorBrush(Colors.Magenta) { Opacity = Constants.InitialEnqueuedVertexOpacity };
        public static Brush IntermediateVertexBrush = new SolidColorBrush(Colors.DarkOrange) { Opacity = Constants.InitialSourceVertexOpacity };
        public static Brush AlreadyPathVertexBrush = new SolidColorBrush(Colors.Gold) { Opacity = Constants.InitialSourceVertexOpacity };
        public static Brush ToReplaceMarkBrush = new SolidColorBrush(Colors.DarkOrange.SetBrightness(72.5)) { Opacity = Constants.InitialSourceVertexOpacity };

        public bool IsVisualizedAsPath(Vertex3D vertex)
        {
            return Dispatcher.Invoke(() => vertex.Brush.IsOneOf(AlreadyPathVertexBrush, PathVertexBrush, IntermediateVertexBrush, ToReplaceMarkBrush));
        }

        public bool IsVisualizedAsRange(Vertex3D vertex)
        {
            return Dispatcher.Invoke(() => vertex.Brush.IsOneOf(SourceVertexBrush, TargetVertexBrush, IntermediateVertexBrush, ToReplaceMarkBrush));
        }

        public void VisualizeAsTarget(Vertex3D vertex)
        {
            Dispatcher.Invoke(() => vertex.Brush = TargetVertexBrush);
        }

        public void VisualizeAsTransit(Vertex3D vertex)
        {
            Dispatcher.Invoke(() => vertex.Brush = IntermediateVertexBrush);
        }

        public void VisualizeAsObstacle(Vertex3D vertex)
        {
            Dispatcher.Invoke(() => vertex.Brush = ObstacleVertexBrush);
        }

        public void VisualizeAsSource(Vertex3D vertex)
        {
            Dispatcher.Invoke(() => vertex.Brush = SourceVertexBrush);
        }

        public void VisualizeAsRegular(Vertex3D vertex)
        {
            Dispatcher.Invoke(() =>
            {
                vertex.Brush = RegularVertexBrush;
            });
        }

        public void VisualizeAsPath(Vertex3D vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsVisualizedAsRange())
                {
                    if (vertex.IsVisualizedAsPath())
                    {
                        vertex.Brush = AlreadyPathVertexBrush;
                    }
                    else
                    {
                        vertex.Brush = PathVertexBrush;
                    }
                }
            });
        }

        public void VisualizeAsVisited(Vertex3D vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsVisualizedAsRange() && !vertex.IsVisualizedAsPath())
                {
                    vertex.Brush = VisitedVertexBrush;
                }
            });
        }

        public void VisualizeAsEnqueued(Vertex3D vertex)
        {
            Dispatcher.Invoke(() =>
            {
                if (!vertex.IsVisualizedAsRange() && !vertex.IsVisualizedAsPath())
                {
                    vertex.Brush = EnqueuedVertexBrush;
                }
            });
        }
    }
}