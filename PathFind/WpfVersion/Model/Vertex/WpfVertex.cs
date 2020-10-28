using GraphLibrary.Coordinates.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Globals;
using GraphLibrary.Info;
using GraphLibrary.Vertex.Cost;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfVersion.Model.Vertex
{
    internal class WpfVertex : Label, IVertex
    {
        public static SolidColorBrush AfterVisitVertexColor { get; set; }
        public static SolidColorBrush PathVertexColor { get; set; }
        public static SolidColorBrush StartVertexColor { get; set; }
        public static SolidColorBrush EndVertexColor { get; set; }
        public static SolidColorBrush EnqueuedVertexColor { get; set; }

        static WpfVertex()
        {
            AfterVisitVertexColor = new SolidColorBrush(Colors.CadetBlue);
            PathVertexColor = new SolidColorBrush(Colors.Yellow);
            StartVertexColor = new SolidColorBrush(Colors.Green);
            EndVertexColor = new SolidColorBrush(Colors.Red);
            EnqueuedVertexColor = new SolidColorBrush(Colors.Brown);
        }

        public WpfVertex() : base()
        {
            this.Initialize();
            Width = Height = VertexParametres.VertexSize;
            FontSize = VertexParametres.VertexSize * VertexParametres.TextToSizeRatio;
            Template = (ControlTemplate)TryFindResource("vertexTemplate");
        }
        public WpfVertex(VertexInfo info) : this()
        {
            this.Initialize(info);
        }

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }
        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }

        private VertexCost cost;
        public VertexCost Cost 
        {
            get { return cost; }
            set { cost = (VertexCost)value.Clone(); Content = cost.ToString(string.Empty); }
        }

        public IList<IVertex> Neighbours { get; set; }
        public IVertex ParentVertex { get; set; }
        public double AccumulatedCost { get; set; }

        private ICoordinate position;
        public ICoordinate Position 
        {
            get => position;
            set { position = value; ToolTip = position.ToString(); }
        }

        public VertexInfo Info => new VertexInfo(this);

        public void MarkAsEnd() => Background = EndVertexColor;

        public void MarkAsObstacle()
        {
            this.WashVertex();
            Background = new SolidColorBrush(Colors.Black);
        }

        public void MarkAsPath()
        {
            Background = PathVertexColor;
        }

        public void MarkAsSimpleVertex()
        {
            if (!IsObstacle)            
                Background = new SolidColorBrush(Colors.White);
        }

        public void MarkAsStart() => Background = StartVertexColor;

        public void MarkAsVisited() => Background = AfterVisitVertexColor;

        public void MarkAsEnqueued()
        {
            Background = EnqueuedVertexColor;
        }

        public void MakeUnweighted()
        {
            Content = string.Empty;
            cost.MakeUnWeighted();
        }

        public void MakeWeighted()
        {
            cost.MakeWeighted();
            Content = cost.ToString(string.Empty);
        }
    }
}
