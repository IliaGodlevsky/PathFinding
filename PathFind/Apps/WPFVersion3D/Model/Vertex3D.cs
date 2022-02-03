using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex3D : UIElement3D, IVertex, IVisualizable, IEquatable<IVertex>
    {
        public static readonly DependencyProperty ModelProperty;
        public static readonly DependencyProperty MaterialProperty;
        public static readonly DependencyProperty SizeProperty;
        public static readonly DependencyProperty BrushProperty;

        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public Model3D Model
        {
            get => (Model3D)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public DiffuseMaterial Material
        {
            get => (DiffuseMaterial)GetValue(MaterialProperty);
            set => SetValue(MaterialProperty, value);
        }

        public Brush Brush
        {
            get => (Brush)GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }

        public Vertex3D(INeighborhood neighborhood, ICoordinate coordinate, IModel3DFactory modelFactory)
        {
            this.modelFactory = modelFactory;
            Position = coordinate;
            Material = new DiffuseMaterial();
            Transform = new TranslateTransform3D();
            Size = Constants.InitialVertexSize;
            this.Initialize();
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(() => neighborhood.GetNeighbours(this));
        }

        public Vertex3D(VertexSerializationInfo info, IModel3DFactory modelFactory) :
            this(info.Neighbourhood, info.Position, modelFactory)
        {
            this.Initialize(info);
        }

        static Vertex3D()
        {
            ModelProperty = DependencyProperty.Register(
                nameof(Model),
                typeof(Model3D),
                typeof(Vertex3D),
                new PropertyMetadata(ModelPropertyChanged));

            MaterialProperty = DependencyProperty.Register(
                nameof(Material),
                typeof(Material),
                typeof(Vertex3D),
                new PropertyMetadata(MaterialPropertyChanged));

            SizeProperty = DependencyProperty.Register(
                nameof(Size),
                typeof(double),
                typeof(Vertex3D),
                new UIPropertyMetadata(SizePropertyChanged));

            BrushProperty = DependencyProperty.Register(
                nameof(Brush),
                typeof(Brush),
                typeof(Vertex3D),
                new PropertyMetadata(BrushPropertyChanged));
        }

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
                else
                {
                    VisualizeAsRegular();
                }
            }
        }

        public IGraph Graph { get; }
        public IVertexCost Cost { get; set; }
        public IReadOnlyCollection<IVertex> Neighbours => neighbours.Value;
        public ICoordinate Position { get; }

        public bool Equals(IVertex other) => other.IsEqual(this);
        public bool IsVisualizedAsPath => ColorsHub.IsVisualizedAsPath(this);
        public bool IsVisualizedAsEndPoint => ColorsHub.IsVisualizedAsEndPoints(this);
        public void VisualizeAsTarget() => ColorsHub.VisualizeAsTarget(this);
        public void VisualizeAsObstacle() => ColorsHub.VisualizeAsObstacle(this);
        public void VisualizeAsPath() => ColorsHub.VisualizeAsPath(this);
        public void VisualizeAsRegular() => ColorsHub.VisualizeAsRegular(this);
        public void VisualizeAsVisited() => ColorsHub.VisualizeAsVisited(this);
        public void VisualizeAsEnqueued() => ColorsHub.VisualizeAsEnqueued(this);
        public void VisualizeAsSource() => ColorsHub.VisualizeAsSource(this);
        public void VisualizeAsIntermediate() => ColorsHub.VisualizeAsIntermediate(this);
        public void VisualizeAsMarkedToReplaceIntermediate() => ColorsHub.VisualizeAsMarkedToReplaceIntermediate(this);

        protected static void MaterialPropertyChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            vert.Material = (DiffuseMaterial)prop.NewValue;
        }

        protected static void SizePropertyChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            double size = (double)prop.NewValue;
            var material = vert.Material;
            vert.Size = size;
            vert.Model = vert.modelFactory.CreateModel3D(size, material);
        }

        protected static void ModelPropertyChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            vert.Visual3DModel = (Model3D)prop.NewValue;
        }

        protected static void BrushPropertyChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            vert.Brush = (SolidColorBrush)prop.NewValue;
            vert.Material.Brush = vert.Brush;
        }

        private readonly IModel3DFactory modelFactory;
        private static readonly VerticesColorsHub ColorsHub = new VerticesColorsHub();
        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;
    }
}