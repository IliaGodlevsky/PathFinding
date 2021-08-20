using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;

using static WPFVersion3D.Constants;

namespace WPFVersion3D.Model
{
    internal class Vertex3D : UIElement3D, IVertex, IVisualizable
    {
        public Vertex3D(INeighboursCoordinates radar, ICoordinate coordinate, IModel3DFactory modelFactory)
        {
            this.modelFactory = modelFactory;
            Position = coordinate;
            NeighboursCoordinates = radar;
            Material = new DiffuseMaterial();
            Transform = new TranslateTransform3D();
            Size = InitialVertexSize;
            this.Initialize();
        }

        public Vertex3D(VertexSerializationInfo info, IModel3DFactory modelFactory) :
            this(info.NeighboursCoordinates, info.Position, modelFactory)
        {
            this.Initialize(info);
        }

        public static SolidColorBrush VisitedVertexBrush { get; set; }
        public static SolidColorBrush ObstacleVertexBrush { get; set; }
        public static SolidColorBrush SimpleVertexBrush { get; set; }
        public static SolidColorBrush PathVertexBrush { get; set; }
        public static SolidColorBrush StartVertexBrush { get; set; }
        public static SolidColorBrush EndVertexBrush { get; set; }
        public static SolidColorBrush EnqueuedVertexBrush { get; set; }
        public static SolidColorBrush IntermediateVertexColor { get; set; }
        public static SolidColorBrush AlreadyPathVertexBrush { get; set; }

        static Vertex3D()
        {
            VisitedVertexBrush = new SolidColorBrush(Colors.CadetBlue) { Opacity = InitialVisitedVertexOpacity };
            PathVertexBrush = new SolidColorBrush(Colors.Yellow) { Opacity = InitialPathVertexOpacity };
            StartVertexBrush = new SolidColorBrush(Colors.Green) { Opacity = InitialStartVertexOpacity };
            EndVertexBrush = new SolidColorBrush(Colors.Red) { Opacity = InitialEndVertexOpacity };
            EnqueuedVertexBrush = new SolidColorBrush(Colors.Magenta) { Opacity = InitialEnqueuedVertexOpacity };
            ObstacleVertexBrush = new SolidColorBrush(Colors.Black) { Opacity = InitialObstacleVertexOpacity };
            SimpleVertexBrush = new SolidColorBrush(Colors.White) { Opacity = InitialRegularVertexOpacity };
            IntermediateVertexColor = new SolidColorBrush(Colors.DarkOrange) { Opacity = InitialStartVertexOpacity };
            AlreadyPathVertexBrush = new SolidColorBrush(Colors.Gold) { Opacity = InitialStartVertexOpacity };

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

        public SolidColorBrush Brush
        {
            get => (SolidColorBrush)GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
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
            }
        }

        public virtual INeighboursCoordinates NeighboursCoordinates { get; }

        public IVertexCost Cost { get; set; }

        public IReadOnlyCollection<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        public bool IsVisualizedAsPath
            => Dispatcher.Invoke(() => Brush.IsOneOf(PathVertexBrush, PathVertexBrush, IntermediateVertexColor));

        public bool IsVisualizedAsEndPoint
            => Dispatcher.Invoke(() => Brush.IsOneOf(StartVertexBrush, EndVertexBrush, IntermediateVertexColor));

        public void VisualizeAsTarget()
        {
            Dispatcher.Invoke(() => Brush = EndVertexBrush);
        }

        public void VisualizeAsObstacle()
        {
            Dispatcher.Invoke(() => Brush = ObstacleVertexBrush);
        }

        public void VisualizeAsPath()
        {
            if(IsVisualizedAsPath)
            {
                Dispatcher.Invoke(() => Brush = AlreadyPathVertexBrush);
            }
            else
            {
                Dispatcher.Invoke(() => Brush = PathVertexBrush);
            }            
        }

        public void VisualizeAsRegular()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsObstacle)
                {
                    Brush = SimpleVertexBrush;
                }
            });
        }

        public void VisualizeAsVisited()
        {
            if (!IsVisualizedAsPath)
            {
                Dispatcher.Invoke(() => Brush = VisitedVertexBrush);
            }
        }

        public void VisualizeAsEnqueued()
        {
            if (!IsVisualizedAsPath)
            {
                Dispatcher.Invoke(() => Brush = EnqueuedVertexBrush);
            }
        }

        public void VisualizeAsSource()
        {
            Dispatcher.Invoke(() => Brush = StartVertexBrush);
        }

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

        public void VisualizeAsIntermediate()
        {
            Dispatcher.Invoke(() => Brush = IntermediateVertexColor);
        }

        private readonly IModel3DFactory modelFactory;
    }
}