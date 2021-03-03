using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Realizations.VertexCost;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal class Vertex : ContentControl, IVertex, IMarkable, IWeightable
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
                Width = Height = VertexSize;
                FontSize = VertexSize * TextToSizeRatio;
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

        public void MarkAsRegular()
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
            (cost as IWeightable)?.MakeUnweighted();
            Dispatcher.Invoke(() => Content = string.Empty);
        }

        public void MakeWeighted()
        {
            (cost as IWeightable)?.MakeWeighted();
            Dispatcher.Invoke(() => Content = cost.ToString());
        }
    }
}
