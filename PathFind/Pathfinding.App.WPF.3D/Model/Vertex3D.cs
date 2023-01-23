using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    public class Vertex3D : UIElement3D, IVertex, IVisualizable
    {
        public static readonly DependencyProperty SizeProperty;
        public static readonly DependencyProperty BrushProperty;

        private readonly IModel3DFactory modelFactory;
        private readonly IVisualization<Vertex3D> visualization;
        private bool isObstacle;

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

        public TranslateTransform3D FieldPosition => (TranslateTransform3D)Transform;

        public IVertexCost Cost { get; set; }

        public IReadOnlyCollection<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; }

        public bool IsVisualizedAsPath() => visualization.IsVisualizedAsPath(this);

        public bool IsVisualizedAsPathfindingRange() => visualization.IsVisualizedAsPathfindingRange(this);

        public DiffuseMaterial Material { get; } = new();

        public Brush Brush
        {
            get => (Brush)GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }

        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public Vertex3D(ICoordinate coordinate, IModel3DFactory modelFactory, IVisualization<Vertex3D> visualization)
        {
            this.visualization = visualization;
            this.modelFactory = modelFactory;
            Position = coordinate;
            Transform = new TranslateTransform3D();
            Size = Constants.InitialVertexSize;
            this.Initialize();
        }

        static Vertex3D()
        {
            SizeProperty = RegisterProperty<double>(nameof(Size), SizePropertyChanged);
            BrushProperty = RegisterProperty<Brush>(nameof(Brush), BrushPropertyChanged);
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

        public void VisualizeAsSource()
        {
            visualization.VisualizeAsSource(this);
        }

        public void VisualizeAsTransit()
        {
            visualization.VisualizeAsTransit(this);
        }

        public void VisualizeAsMarkedToReplaceIntermediate()
        {
            visualization.VisualizeAsMarkedToReplaceIntermediate(this);
        }

        protected static void SizePropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            vert.Size = (double)prop.NewValue;
            vert.Visual3DModel = vert.modelFactory.CreateModel3D(vert.Size, vert.Material);
        }

        protected static void BrushPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            vert.Brush = (Brush)prop.NewValue;
            vert.Material.Brush = vert.Brush;
        }

        private static DependencyProperty RegisterProperty<T>(string propertyName, PropertyChangedCallback callback)
        {
            return DependencyProperty.Register(propertyName, typeof(T), typeof(Vertex3D), new(callback));
        }
    }
}