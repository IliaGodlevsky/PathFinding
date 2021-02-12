using GraphLib.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using GraphLib.VertexCost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFVersion.Model
{
    internal class Vertex : Label, IVertex, IMarkableVertex, IWeightableVertex
    {
        public static SolidColorBrush VisitedVertexColor { get; set; }
        public static SolidColorBrush PathVertexColor { get; set; }
        public static SolidColorBrush StartVertexColor { get; set; }
        public static SolidColorBrush EndVertexColor { get; set; }
        public static SolidColorBrush EnqueuedVertexColor { get; set; }

        static Vertex()
        {
            VisitedVertexColor = new SolidColorBrush(Colors.CadetBlue);
            PathVertexColor = new SolidColorBrush(Colors.Yellow);
            StartVertexColor = new SolidColorBrush(Colors.Green);
            EndVertexColor = new SolidColorBrush(Colors.Red);
            EnqueuedVertexColor = new SolidColorBrush(Colors.Magenta);
        }

        public Vertex() : base()
        {
            Dispatcher.Invoke(() =>
            {
                double fontSizeRatio = Convert.ToDouble(ConfigurationManager.AppSettings["textToSizeRatio"]);
                int vertexSize = Convert.ToInt32(ConfigurationManager.AppSettings["vertexSize"]);
                Width = Height = vertexSize;
                FontSize = vertexSize * fontSizeRatio;
                Template = (ControlTemplate)TryFindResource("vertexTemplate");
            });
            this.Initialize();
        }

        public Vertex(VertexSerializationInfo info) : this()
        {
            this.Initialize(info);
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

        private IVertexCost cost;
        public IVertexCost Cost
        {
            get => cost;
            set
            {
                cost = value;
                Dispatcher.Invoke(() => Content = cost.ToString());
            }
        }

        public IList<IVertex> Neighbours { get; set; }

        private ICoordinate position;
        public ICoordinate Position
        {
            get => position;
            set
            {
                position = value;
                Dispatcher.Invoke(() => ToolTip = position.ToString());
            }
        }

        public bool IsDefault => false;

        public void MarkAsEnd()
        {
            Dispatcher.Invoke(() => Background = EndVertexColor);
        }

        public void MarkAsObstacle()
        {
            Dispatcher.Invoke(() => Background = new SolidColorBrush(Colors.Black));
        }

        public void MarkAsPath()
        {
            Dispatcher.Invoke(() => Background = PathVertexColor);
        }

        public void MarkAsStart()
        {
            Dispatcher.Invoke(() => Background = StartVertexColor);
        }

        public void MarkAsSimpleVertex()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsObstacle)
                {
                    Background = new SolidColorBrush(Colors.White);
                }
            });
        }

        public void MarkAsVisited()
        {
            Dispatcher.Invoke(() => Background = VisitedVertexColor);
        }

        public void MarkAsEnqueued()
        {
            Dispatcher.Invoke(() => Background = EnqueuedVertexColor);
        }

        public void MakeUnweighted()
        {
            (cost as Cost).MakeUnWeighted();
            Dispatcher.Invoke(() => Content = string.Empty);
        }

        public void MakeWeighted()
        {
            (cost as Cost).MakeWeighted();
            Dispatcher.Invoke(() => Content = cost.ToString());
        }
    }
}
