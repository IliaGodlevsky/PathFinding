using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex : ContentControl, IVertex, IVisualizable
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
        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;

        private IVertexCost cost;
        private bool isObstacle;
        private ICoordinate position;

        public bool IsVisualizedAsPath => visualization.IsVisualizedAsPath(this);

        public bool IsVisualizedAsEndPoint => visualization.IsVisualizedAsEndPoint(this);

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
                    RaiseEvent(new RoutedEventArgs(EnqueuedEvent, this));
                }
                if (visualization.IsVisualizedAsPath(this))
                {
                    RaiseEvent(new RoutedEventArgs(ColoredAsPathEvent, this));
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

        public IReadOnlyCollection<IVertex> Neighbours => neighbours.Value;

        public IGraph Graph { get; }

        public Vertex(IReadOnlyCollection<ICoordinate> neighborhood, ICoordinate coordinate, IVisualization<Vertex> visualization) : base()
        {
            RenderTransformOrigin = new Point(0.5, 0.5);
            RenderTransform = new ScaleTransform();
            this.visualization = (VertexVisualization)visualization;
            Width = Height = VertexSize;
            Template = (ControlTemplate)TryFindResource("VertexTemplate");
            Position = coordinate;
            this.Initialize();
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(() => neighborhood.GetNeighboursWithinGraph(this));
        }

        public Vertex(VertexSerializationInfo info, IVisualization<Vertex> visualization)
            : this(info.Neighbourhood, info.Position, visualization)
        {
            this.Initialize(info);
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

        public void VisualizeAsIntermediate()
        {
            visualization.VisualizeAsIntermediate(this);
        }

        public void VisualizeAsMarkedToReplaceIntermediate()
        {
            visualization.VisualizeAsMarkedToReplaceIntermediate(this);
        }

        private static RoutedEvent RegisterRoutedEvent(string name)
        {
            return EventManager.RegisterRoutedEvent(name, RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Vertex));
        }
    }
}