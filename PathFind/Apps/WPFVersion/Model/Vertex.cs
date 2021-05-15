using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using NullObject.Extensions;
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

        public Vertex(ICoordinateRadar radar,
            ICoordinate coordinate) : base()
        {
            Dispatcher.Invoke(() =>
            {
                Width = Height = VertexSize;
                FontSize = VertexSize * TextToSizeRatio;
                Template = (ControlTemplate)TryFindResource("vertexTemplate");
            });
            this.Initialize();
            Position = coordinate;
            CoordinateRadar = radar;

        }

        public Vertex(VertexSerializationInfo info, ICoordinateRadar radar)
            : this(radar, info.Position)
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

        public virtual ICoordinateRadar CoordinateRadar { get; }

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

        public ICollection<IVertex> Neighbours { get; set; }

        private ICoordinate position;
        public ICoordinate Position
        {
            get => position;
            private set
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
                    if (!cost.IsNullObject())
                    {
                        Background = new SolidColorBrush(Colors.White);
                    }
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

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }
    }
}
