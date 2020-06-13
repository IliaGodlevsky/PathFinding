using GraphLibrary;
using GraphLibrary.Constants;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfVersion.Model.Vertex
{
    public class WpfVertex : Button, IVertex
    {
        public static SolidColorBrush AfterVisitVertexColor { get; set; }
        public static SolidColorBrush BeingVisitedVertexColor { get; set; }
        public static SolidColorBrush PathVertexColor { get; set; }
        public static SolidColorBrush StartVertexColor { get; set; }
        public static SolidColorBrush EndVertexColor { get; set; }

        static WpfVertex()
        {
            AfterVisitVertexColor = new SolidColorBrush(Colors.CadetBlue);
            BeingVisitedVertexColor = new SolidColorBrush(Colors.DarkMagenta);
            PathVertexColor = new SolidColorBrush(Colors.Yellow);
            StartVertexColor = new SolidColorBrush(Colors.Green);
            EndVertexColor = new SolidColorBrush(Colors.Red);
        }

        public WpfVertex() : base()
        {
            Neighbours = new List<IVertex>();
            SetToDefault();
            Width = Const.WIN_FORMS_VERTEX_SIZE;
            Height = Const.WIN_FORMS_VERTEX_SIZE;
            IsObstacle = false;
            FontSize = 12;
            Template = (ControlTemplate)TryFindResource("roundbutton");
        }

        public WpfVertex(VertexInfo info) : this()
        {
            IsObstacle = info.IsObstacle;
            Text = info.Text;
            if (IsObstacle)
                MarkAsObstacle();
        }

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }

        public bool IsSimpleVertex => !IsEnd && !IsStart;

        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }
        public string Text 
        { 
            get{return Content.ToString();}
            set{Content = value;}
        }
        public List<IVertex> Neighbours { get; set; }
        public IVertex ParentVertex { get; set; }
        public double Value { get; set; }
        public System.Drawing.Point Location { get; set; }

        public VertexInfo Info => new VertexInfo(this);

        public void MarkAsCurrentlyLooked()
        {
            Background = BeingVisitedVertexColor;
        }

        public void MarkAsEnd()
        {
            Background = EndVertexColor;
        }

        public void MarkAsObstacle()
        {
            Text = "";
            IsObstacle = true;
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

        public void MarkAsStart()
        {
            Background = StartVertexColor;
        }

        public void MarkAsVisited()
        {
            Background = AfterVisitVertexColor;
        }

        public void SetToDefault()
        {
            IsStart = false;
            IsEnd = false;
            IsVisited = false;
            Value = 0;
            MarkAsSimpleVertex();
            ParentVertex = null;
        }
    }
}
