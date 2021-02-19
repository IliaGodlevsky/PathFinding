using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPFVersion3D.Factories;

namespace WPFVersion3D.Model
{
    internal class Vertex3D : UIElement3D, IVertex, IMarkableVertex
    {
        public Vertex3D()
        {
            Dispatcher.Invoke(() =>
            {
                Size = 5;
                Material = new DiffuseMaterial();
                Model = Model3DFactory.CreateCubicModel3D(Size, Material);
                Transform = new TranslateTransform3D();
            });
            this.Initialize();
        }

        public Vertex3D(VertexSerializationInfo info) : this()
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

            VisitedVertexBrush = new SolidColorBrush(Colors.CadetBlue) { Opacity = 0.15 };
            PathVertexBrush = new SolidColorBrush(Colors.Yellow) { Opacity = 0.9 };
            StartVertexBrush = new SolidColorBrush(Colors.Green) { Opacity = 1 };
            EndVertexBrush = new SolidColorBrush(Colors.Red) { Opacity = 1 };
            EnqueuedVertexBrush = new SolidColorBrush(Colors.Magenta) { Opacity = 0.15 };
            ObstacleVertexBrush = new SolidColorBrush(Colors.Black) { Opacity = 0.2 };
            SimpleVertexBrush = new SolidColorBrush(Colors.White) { Opacity = 0.25 };

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

        public IVertexCost Cost { get; set; }

        public IList<IVertex> Neighbours { get; set; }

        public ICoordinate Position { get; set; }

        public bool IsDefault => false;

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

        public void MarkAsSimpleVertex()
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
            vert.Size = (double)prop.NewValue;
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
            (vert.Material as DiffuseMaterial).Brush = vert.Brush;
        }
    }
}
