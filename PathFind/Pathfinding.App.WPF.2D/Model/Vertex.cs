using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Pathfinding.App.WPF._2D.Constants;

namespace Pathfinding.App.WPF._2D.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    public class Vertex : ContentControl, IVertex, ITotallyVisualizable
    {
        public static readonly RoutedEvent EnqueuedEvent;
        public static readonly RoutedEvent ColoredAsPathEvent;

        public event RoutedEventHandler Enqueued
        {
            add => AddHandler(EnqueuedEvent, value);
            remove => RemoveHandler(EnqueuedEvent, value);
        }

        public event RoutedEventHandler ColoredAsPath
        {
            add => AddHandler(ColoredAsPathEvent, value);
            remove => RemoveHandler(ColoredAsPathEvent, value);
        }

        private readonly VertexVisualization visualization;

        private IVertexCost cost;
        private bool isObstacle;
        private ICoordinate position;

        public bool IsVisualizedAsRange() => visualization.IsVisualizedAsRange(this);

        public bool IsVisualizedAsPath() => visualization.IsVisualizedAsPath(this);

        public bool IsObstacle
        {
            get => isObstacle;
            set
            {
                isObstacle = value;
                if (isObstacle)
                {
                    VisualizeAsObstacle();
                }
            }
        }

        public Brush VertexColor
        {
            get => Background;
            set
            {
                Background = value;
                if (visualization.IsVisualizedAsEnqueued(this))
                {
                    RaiseEvent(new(EnqueuedEvent, this));
                }
                if (visualization.IsVisualizedAsPath(this))
                {
                    RaiseEvent(new(ColoredAsPathEvent, this));
                }
            }
        }

        public IVertexCost Cost
        {
            get => cost;
            set
            {
                cost = value;
                Dispatcher.Invoke(() => Content = cost.CurrentCost.ToString());
            }
        }

        public ICoordinate Position
        {
            get => position;
            private set
            {
                position = value;
                Dispatcher.Invoke(() => ToolTip = position.ToString());
            }
        }

        public IList<IVertex> Neighbours { get; set; }

        public Vertex(ICoordinate coordinate, ITotalVisualization<Vertex> visualization) : base()
        {
            RenderTransformOrigin = new Point(0.5, 0.5);
            RenderTransform = new ScaleTransform();
            this.visualization = (VertexVisualization)visualization;
            Width = Height = VertexSize;
            Template = (ControlTemplate)TryFindResource("VertexTemplate");
            Position = coordinate;
            this.Initialize();
        }

        static Vertex()
        {
            EnqueuedEvent = RegisterRoutedEvent(nameof(Enqueued));
            ColoredAsPathEvent = RegisterRoutedEvent(nameof(ColoredAsPath));
        }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        public void VisualizeAsTarget()
        {
            visualization.VisualizeAsTarget(this);
        }

        public void VisualizeAsObstacle()
        {
            visualization.VisualizeAsObstacle(this);
        }

        public void VisualizeAsPath()
        {
            visualization.VisualizeAsPath(this);
        }

        public void VisualizeAsSource()
        {
            visualization.VisualizeAsSource(this);
        }

        public void VisualizeAsRegular()
        {
            visualization.VisualizeAsRegular(this);
        }

        public void VisualizeAsVisited()
        {
            visualization.VisualizeAsVisited(this);
        }

        public void VisualizeAsEnqueued()
        {
            visualization.VisualizeAsEnqueued(this);
        }

        public void VisualizeAsTransit()
        {
            visualization.VisualizeAsTransit(this);
        }

        private static RoutedEvent RegisterRoutedEvent(string name)
        {
            return EventManager.RegisterRoutedEvent(name, RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Vertex));
        }
    }
}