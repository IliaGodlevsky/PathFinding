using Common;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using GraphLib.Info;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfVersion.Model.Vertex
{
    internal class WpfVertex : Label, IVertex
    {
        public static SolidColorBrush VisitedVertexColor { get; set; }
        public static SolidColorBrush PathVertexColor { get; set; }
        public static SolidColorBrush StartVertexColor { get; set; }
        public static SolidColorBrush EndVertexColor { get; set; }
        public static SolidColorBrush EnqueuedVertexColor { get; set; }

        static WpfVertex()
        {
            VisitedVertexColor = new SolidColorBrush(Colors.CadetBlue);
            PathVertexColor = new SolidColorBrush(Colors.Yellow);
            StartVertexColor = new SolidColorBrush(Colors.Green);
            EndVertexColor = new SolidColorBrush(Colors.Red);
            EnqueuedVertexColor = new SolidColorBrush(Colors.Magenta);
        }

        public WpfVertex() : base()
        {
            Dispatcher.InvokeAsync(() =>
            {
                Width = Height = VertexParametres.VertexSize;
                FontSize = VertexParametres.VertexSize * VertexParametres.TextToSizeRatio;
                Template = (ControlTemplate)TryFindResource("vertexTemplate");
            });
            this.Initialize();
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
            set 
            { 
                cost = (VertexCost)value.Clone();
                Dispatcher.InvokeAsync(() => Content = cost.ToString(string.Empty));
            }
        }

        public IList<IVertex> Neighbours { get; set; }

        public IVertex ParentVertex { get; set; }

        public double AccumulatedCost { get; set; }

        private ICoordinate position;
        public ICoordinate Position 
        {
            get => position;
            set 
            { 
                position = value;
                Dispatcher.InvokeAsync(() => ToolTip = position.ToString());
            }
        }

        public bool IsDefault => false;

        public void MarkAsEnd()
        {
            Dispatcher.InvokeAsync(() => Background = EndVertexColor);
        }

        public void MarkAsObstacle()
        {
            this.WashVertex();
            Dispatcher.InvokeAsync(() => Background = new SolidColorBrush(Colors.Black));
        }

        public void MarkAsPath()
        {
            Dispatcher.InvokeAsync(() => Background = PathVertexColor);
        }

        public void MarkAsStart()
        {
            Dispatcher.InvokeAsync(() => Background = StartVertexColor);
        }

        public void MarkAsSimpleVertex()
        {
            Dispatcher.InvokeAsync(() =>
            {
                if (!IsObstacle)
                {
                    Background = new SolidColorBrush(Colors.White);
                }
            });
        }

        public void MarkAsVisited()
        {
            Dispatcher.InvokeAsync(() => Background = VisitedVertexColor);
        }

        public void MarkAsEnqueued()
        {
            Dispatcher.InvokeAsync(() => Background = EnqueuedVertexColor);
        }

        public void MakeUnweighted()
        {
            Dispatcher.InvokeAsync(() => Content = string.Empty);
            cost.MakeUnWeighted();
        }

        public void MakeWeighted()
        {
            Dispatcher.InvokeAsync(() => cost.MakeWeighted());
            Content = cost.ToString(string.Empty);
        }
    }
}
