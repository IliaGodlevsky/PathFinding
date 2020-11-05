using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using GraphLib.Info;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

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

        public static MaterialGroup AfterVisitVertexColor { get; set; }
        public static MaterialGroup ObstacleVertexColor { get; set; }
        public static MaterialGroup SimpleVertexColor { get; set; }
        public static MaterialGroup PathVertexColor { get; set; }
        public static MaterialGroup StartVertexColor { get; set; }
        public static MaterialGroup EndVertexColor { get; set; }
        public static MaterialGroup EnqueuedVertexColor { get; set; }

        static Wpf3dVertex()
        {
            AfterVisitVertexColor = GetMaterial(Colors.CadetBlue, opacity: 0.25);
            PathVertexColor = GetMaterial(Colors.Yellow, opacity: 0.9);
            StartVertexColor = GetMaterial(Colors.Green, opacity: 0.5);
            EndVertexColor = GetMaterial(Colors.Red, opacity: 0.5);
            EnqueuedVertexColor = GetMaterial(Colors.Brown, opacity: 0.25);
            ObstacleVertexColor = GetMaterial(Colors.Black, opacity: 0.25);
            SimpleVertexColor = GetMaterial(Colors.White, opacity: 0.35);

            ModelProperty = DependencyProperty.Register("Model", typeof(Model3D),
                typeof(Wpf3dVertex), new PropertyMetadata(ModelPropertyChanged));
            MaterialProperty = DependencyProperty.Register("Material", typeof(Material),typeof(Wpf3dVertex), 
                new PropertyMetadata(new DiffuseMaterial(Brushes.White), VisualPropertyChanged));
            SizeProperty = DependencyProperty.Register("Size", typeof(double),
                typeof(Wpf3dVertex), new UIPropertyMetadata(25d, VisualPropertyChanged));
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

        }

        public void MakeWeighted()
        {

        }

        public void MarkAsEnd()
        {
            Material = EndVertexColor;
        }

        public void MarkAsEnqueued()
        {
            Material = EnqueuedVertexColor;
        }

        public void MarkAsObstacle()
        {
            this.WashVertex();
            Material = ObstacleVertexColor;
        }

        public void MarkAsPath()
        {
            Material = PathVertexColor;
        }

        public void MarkAsSimpleVertex()
        {
            if (!IsObstacle)
                Material = SimpleVertexColor;
            
        }

        public void MarkAsStart()
        {
            Material = StartVertexColor;
        }

        public void MarkAsVisited()
        {
            Material = AfterVisitVertexColor;
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
            Model = CreateElementModel();
        }

        protected Model3D CreateElementModel()
        {
            Model3DGroup model = new Model3DGroup();

            var p0 = new Point3D(0d, 0d, 0d);
            var p1 = new Point3D(Size, 0d, 0d);
            var p2 = new Point3D(Size, 0d, Size);
            var p3 = new Point3D(0d, 0d, Size);
            var p4 = new Point3D(0d, Size, Size);
            var p5 = new Point3D(Size, Size, Size);
            var p6 = new Point3D(Size, Size, 0d);
            var p7 = new Point3D(0d, Size, 0d);

            model.Children.Add(CreateRectangleModel(p4, p3, p2, p5, Material));
            model.Children.Add(CreateRectangleModel(p5, p2, p1, p6, Material));
            model.Children.Add(CreateRectangleModel(p7, p6, p1, p0, Material));
            model.Children.Add(CreateRectangleModel(p7, p0, p3, p4, Material));
            model.Children.Add(CreateRectangleModel(p7, p4, p5, p6, Material));
            model.Children.Add(CreateRectangleModel(p0, p1, p2, p3, Material));

            return model;
        }

        protected static MeshGeometry3D CreateTriangleMesh(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            return mesh;
        }

        private static MeshGeometry3D CreateRectangleMesh(Point3D p0, Point3D p1, Point3D p2, Point3D p3)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            return mesh;
        }

        protected static GeometryModel3D CreateTriangleModel(Point3D p0, Point3D p1, Point3D p2, Material material)
        {
            return new GeometryModel3D(CreateTriangleMesh(p0, p1, p2), material);
        }

        protected static GeometryModel3D CreateRectangleModel(Point3D p0, Point3D p1, Point3D p2, Point3D p3, Material material)
        {
            return new GeometryModel3D(CreateRectangleMesh(p0, p1, p2, p3), material);
        }

        private static MaterialGroup GetMaterial(Color color, double opacity)
        {
            var materailGroup = new MaterialGroup();

            var diffuseMaterial = new DiffuseMaterial
            {
                Brush = new SolidColorBrush(color)
            };
            diffuseMaterial.Brush.Opacity = opacity;
            materailGroup.Children.Add(diffuseMaterial);

            return materailGroup;
        }
    }
}
