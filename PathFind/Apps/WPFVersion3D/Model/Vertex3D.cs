using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static WPFVersion3D.Constants;

namespace WPFVersion3D.Model
{
    internal class Vertex3D : UIElement3D, IVertex, IMarkable
    {
        public Vertex3D(ICoordinateRadar radar,
            ICoordinate coordinate)
        {
            Dispatcher.Invoke(() =>
            {
                Size = InitialVertexSize;
                Material = new DiffuseMaterial();
                Model = Model3DFactory.CreateCubicModel3D(Size, Material);
                Transform = new TranslateTransform3D();
            });
            this.Initialize();
            Position = coordinate;
            CoordinateRadar = radar;

        }

        public Vertex3D(VertexSerializationInfo info, ICoordinateRadar radar) :
            this(radar, info.Position)
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

        static Vertex3D()
        {
            VisitedVertexBrush = new SolidColorBrush(Colors.CadetBlue) { Opacity = InitialVisitedVertexOpacity };
            PathVertexBrush = new SolidColorBrush(Colors.Yellow) { Opacity = InitialPathVertexOpacity };
            StartVertexBrush = new SolidColorBrush(Colors.Green) { Opacity = InitialStartVertexOpacity };
            EndVertexBrush = new SolidColorBrush(Colors.Red) { Opacity = InitialEndVertexOpacity };
            EnqueuedVertexBrush = new SolidColorBrush(Colors.Magenta) { Opacity = InitialEnqueuedVertexOpacity };
            ObstacleVertexBrush = new SolidColorBrush(Colors.Black) { Opacity = InitialObstacleVertexOpacity };
            SimpleVertexBrush = new SolidColorBrush(Colors.White) { Opacity = InitialRegularVertexOpacity };

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

        public Material Material
        {
            get => (Material)GetValue(MaterialProperty);
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
                    MarkAsObstacle();
            }
        }

        public virtual ICoordinateRadar CoordinateRadar { get; }

        public IVertexCost Cost { get; set; }

        public ICollection<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        public void MarkAsEnd()
        {
            Dispatcher.Invoke(() => Brush = EndVertexBrush);
        }

        public void MarkAsObstacle()
        {
            Dispatcher.Invoke(() => Brush = ObstacleVertexBrush);
        }

        public void MarkAsPath()
        {
            Dispatcher.Invoke(() => Brush = PathVertexBrush);
        }

        public void MarkAsRegular()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsObstacle)
                {
                    Brush = SimpleVertexBrush;
                }
            });
        }

        public void MarkAsVisited()
        {
            Dispatcher.Invoke(() => Brush = VisitedVertexBrush);
        }

        public void MarkAsEnqueued()
        {
            Dispatcher.Invoke(() => Brush = EnqueuedVertexBrush);
        }

        public void MarkAsStart()
        {
            Dispatcher.Invoke(() => Brush = StartVertexBrush);
        }

        protected static void MaterialPropertyChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            vert.Material = (Material)prop.NewValue;
        }

        protected static void SizePropertyChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            double size = (double)prop.NewValue;
            var material = vert.Material;
            vert.Size = size;
            vert.Model = Model3DFactory.CreateCubicModel3D(size, material);
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
            ((DiffuseMaterial)vert.Material).Brush = vert.Brush;
        }
    }
}
