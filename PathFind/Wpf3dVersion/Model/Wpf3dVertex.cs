using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using GraphLib.Info;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
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
            this.Initialize();
            Size = 5;            
        }

        public Wpf3dVertex(VertexInfo info) : this()
        {
            this.Initialize(info);
        }

        public static DiffuseMaterial VisitedVertexMaterial { get; set; }
        public static DiffuseMaterial ObstacleVertexMaterial { get; set; }
        public static DiffuseMaterial SimpleVertexMaterial { get; set; }
        public static DiffuseMaterial PathVertexMaterial { get; set; }
        public static DiffuseMaterial StartVertexMaterial { get; set; }
        public static DiffuseMaterial EndVertexMaterial { get; set; }
        public static DiffuseMaterial EnqueuedVertexMaterial { get; set; }

        static Wpf3dVertex()
        {
            VisitedVertexMaterial = MaterialFactory.GetDiffuseMaterial(Colors.CadetBlue, opacity: 0.15);
            PathVertexMaterial = MaterialFactory.GetDiffuseMaterial(Colors.Yellow, opacity: 0.9);
            StartVertexMaterial = MaterialFactory.GetDiffuseMaterial(Colors.Green, opacity: 0.5);
            EndVertexMaterial = MaterialFactory.GetDiffuseMaterial(Colors.Red, opacity: 0.5);
            EnqueuedVertexMaterial = MaterialFactory.GetDiffuseMaterial(Colors.Magenta, opacity: 0.15);
            ObstacleVertexMaterial = MaterialFactory.GetDiffuseMaterial(Colors.Black, opacity: 0.2);
            SimpleVertexMaterial = MaterialFactory.GetDiffuseMaterial(Colors.White, opacity: 0.25);

            ModelProperty = DependencyProperty.Register(nameof(Model), typeof(Model3D),
                typeof(Wpf3dVertex), new PropertyMetadata(ModelPropertyChanged));
            MaterialProperty = DependencyProperty.Register(nameof(Material), typeof(Material),
                typeof(Wpf3dVertex), new PropertyMetadata(VisualPropertyChanged));
            SizeProperty = DependencyProperty.Register(nameof(Size), typeof(double),
                typeof(Wpf3dVertex), new UIPropertyMetadata(VisualPropertyChanged));
        }

        public static readonly DependencyProperty ModelProperty;
        public static readonly DependencyProperty MaterialProperty;
        public static readonly DependencyProperty SizeProperty;

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
            Material = EndVertexMaterial;
        }

        public void MarkAsEnqueued()
        {
            Material = EnqueuedVertexMaterial;
        }

        public void MarkAsObstacle()
        {
            this.WashVertex();
            Material = ObstacleVertexMaterial;
        }

        public void MarkAsPath()
        {
            Material = PathVertexMaterial;
        }

        public void MarkAsSimpleVertex()
        {
            if (!IsObstacle)
                Material = SimpleVertexMaterial;
            
        }

        public void MarkAsStart()
        {
            Material = StartVertexMaterial;
        }

        public void MarkAsVisited()
        {
            Material = VisitedVertexMaterial;
        }

        protected static void VisualPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs prop)
        {
            Wpf3dVertex vert = (Wpf3dVertex)depObj;
            vert.InvalidateModel();
        }

        protected static void ModelPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs prop)
        {
            Wpf3dVertex vert = (Wpf3dVertex)depObj;
            vert.Visual3DModel = vert.Model;
        }

        protected override void OnUpdateModel()
        {
            Model = Model3DFactory.GetCubicModel3D(Size, Material);
        }
    }
}
