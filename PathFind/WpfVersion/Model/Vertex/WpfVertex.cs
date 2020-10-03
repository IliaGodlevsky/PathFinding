using GraphLibrary.Coordinates;
using GraphLibrary.DTO;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Globals;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfVersion.Model.Vertex
{
    [Serializable]
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

        public WpfVertex(VertexDto info) : this() => this.Initialize(info);

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }
        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }

        public int Cost 
        {
            get { return int.Parse(Content.ToString()); }
            set { Content = value.ToString(); }
        }

        public IList<IVertex> Neighbours { get; set; }
        public IVertex ParentVertex { get; set; }
        public double AccumulatedCost { get; set; }
        public Position Position { get; set; }

        public VertexDto Dto => new VertexDto(this);

        public void MarkAsEnd() => Background = EndVertexColor;

        public void MarkAsObstacle()
        {
            this.WashVertex();
            Background = new SolidColorBrush(Colors.Black);
        }

        public void MarkAsPath() => Background = PathVertexColor;

        public void MarkAsSimpleVertex()
        {
            if (!IsObstacle)
                Background = new SolidColorBrush(Colors.White);
        }

        public void MarkAsStart() => Background = StartVertexColor;

        public void MarkAsVisited() => Background = AfterVisitVertexColor;

        public void MarkAsEnqueued() => Background = EnqueuedVertexColor;
    }
}
