using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using GraphLib.Info;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Wpf3dVersion.Factories;

namespace Wpf3dVersion.Model
{
    public class Wpf3dVertex : UIElement3D, IVertex
    {
        public Wpf3dVertex()
        {           
            Size = 5;
            Material = new DiffuseMaterial();
            Model = Model3DFactory.CreateCubicModel3D(Size, Material);
            this.Initialize();
        }

        public Wpf3dVertex(VertexInfo info) : this()
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

        static Wpf3dVertex()
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
                typeof(Wpf3dVertex), 
                new PropertyMetadata(ModelPropertyChanged));

            MaterialProperty = DependencyProperty.Register(
                nameof(Material), 
                typeof(Material),
                typeof(Wpf3dVertex), 
                new PropertyMetadata(VisualPropertyChanged));

            SizeProperty = DependencyProperty.Register(
                nameof(Size), 
                typeof(double),
                typeof(Wpf3dVertex), 
                new UIPropertyMetadata(VisualPropertyChanged));

            BrushProperty = DependencyProperty.Register(
                nameof(Brush), 
                typeof(Brush),
                typeof(Wpf3dVertex), 
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

        public bool IsEnd { get; set; }

        public bool IsObstacle { get; set; }

        public bool IsStart { get; set; }

        public bool IsVisited { get; set; }


        private VertexCost cost;
        public VertexCost Cost
        {
            get { return cost; }
            set { cost = (VertexCost)value.Clone(); }
        }

        public IList<IVertex> Neighbours { get; set; }

        public IVertex ParentVertex { get; set; }

        public double AccumulatedCost { get; set; }

        public ICoordinate Position { get; set; }

        public VertexInfo Info => new VertexInfo(this);

        public bool IsDefault => false;

        public void MakeUnweighted()
        {
            Cost.MakeUnWeighted();
        }

        public void MakeWeighted()
        {
            Cost.MakeWeighted();
        }

        public void MarkAsEnd()
        {
            Brush = EndVertexBrush;
        }

        public void MarkAsEnqueued()
        {
            Brush = EnqueuedVertexBrush;
        }

        public void MarkAsObstacle()
        {
            this.WashVertex();
            Brush = ObstacleVertexBrush;
        }

        public void MarkAsPath()
        {
            Brush = PathVertexBrush;
            
        }

        public void MarkAsSimpleVertex()
        {
            if (!IsObstacle)
            {
                Brush = SimpleVertexBrush;
            }
        }

        public void MarkAsStart()
        {
            Brush = StartVertexBrush;
        }

        public void MarkAsVisited()
        {
            Brush = VisitedVertexBrush;
        }

        protected async static void VisualPropertyChanged(DependencyObject depObj, 
            DependencyPropertyChangedEventArgs prop)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var method = depObj.GetType().GetMethod(nameof(InvalidateModel), flags);
            var delegatedMethod = Delegate.CreateDelegate(typeof(Action), depObj, method);
            await depObj.Dispatcher.BeginInvoke(delegatedMethod);
        }

        protected static void ModelPropertyChanged(DependencyObject depObj, 
            DependencyPropertyChangedEventArgs prop)
        {
            Wpf3dVertex vert = (Wpf3dVertex)depObj;
            vert.Visual3DModel = vert.Model;
        }

        protected async static void BrushPropertyChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs prop)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var method = depObj.GetType().GetMethod(nameof(OnUpdateModel), flags);
            var delegatedMethod = Delegate.CreateDelegate(typeof(Action), depObj, method);
            await depObj.Dispatcher.BeginInvoke(delegatedMethod);
        }

        protected override void OnUpdateModel()
        {
            GeometryModel3D child = null;
            if (Model is Model3DGroup modelGroup)
            {
               child = modelGroup.Children.FirstOrDefault() as GeometryModel3D;
            }

            DiffuseMaterial material = null;
            if (child != null) 
            {
                 material = child.Material as DiffuseMaterial;
            }

            if (material != null)
            {
                material.Brush = Brush;
            }
        }
    }    
}
