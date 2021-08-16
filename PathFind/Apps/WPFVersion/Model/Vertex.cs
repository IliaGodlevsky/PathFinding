using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal class Vertex : Label, IVertex, IMarkable, IWeightable
    {
        public static SolidColorBrush VisitedVertexColor { get; set; }
        public static SolidColorBrush PathVertexColor { get; set; }
        public static SolidColorBrush StartVertexColor { get; set; }
        public static SolidColorBrush EndVertexColor { get; set; }
        public static SolidColorBrush EnqueuedVertexColor { get; set; }
        public static SolidColorBrush ObstacleVertexColor { get; set; }
        public static SolidColorBrush RegularVertexColor { get; set; }
        public static SolidColorBrush AlreadyPathVertexColor { get; set; }
        public static SolidColorBrush IntermediateVertexColor { get; set; }

        static Vertex()
        {
            VisitedVertexColor = new SolidColorBrush(Colors.CadetBlue);
            PathVertexColor = new SolidColorBrush(Colors.Yellow);
            StartVertexColor = new SolidColorBrush(Colors.Green);
            EndVertexColor = new SolidColorBrush(Colors.Red);
            EnqueuedVertexColor = new SolidColorBrush(Colors.Magenta);
            ObstacleVertexColor = new SolidColorBrush(Colors.Black);
            RegularVertexColor = new SolidColorBrush(Colors.White);
            AlreadyPathVertexColor = new SolidColorBrush(Colors.Gold);
            IntermediateVertexColor = new SolidColorBrush(Colors.DarkOrange);
        }

        public Vertex(INeighboursCoordinates radar, ICoordinate coordinate) : base()
        {
            Width = Height = VertexSize;
            Template = (ControlTemplate)TryFindResource("vertexTemplate");
            Position = coordinate;
            NeighboursCoordinates = radar;
            this.Initialize();
        }

        public Vertex(VertexSerializationInfo info)
            : this(info.NeighboursCoordinates, info.Position)
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
                {
                    MarkAsObstacle();
                }
            }
        }

        public virtual INeighboursCoordinates NeighboursCoordinates { get; }

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

        public IReadOnlyCollection<IVertex> Neighbours { get; set; }

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

        public void MarkAsTarget()
        {
            Dispatcher.Invoke(() => Background = EndVertexColor);
        }

        public void MarkAsObstacle()
        {
            Dispatcher.Invoke(() => Background = ObstacleVertexColor);
        }

        public void MarkAsPath()
        {
            Dispatcher.Invoke(() =>
            {
                if (IsMarkedAsPathed())
                {
                    Background = AlreadyPathVertexColor;
                }
                else
                {
                    Background = PathVertexColor;
                }
            });
        }

        public void MarkAsSource()
        {
            Dispatcher.Invoke(() => Background = StartVertexColor);
        }

        public void MarkAsRegular()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsObstacle)
                {
                    Background = RegularVertexColor;
                }
            });
        }

        public void MarkAsVisited()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsMarkedAsPathed())
                {
                    Background = VisitedVertexColor;
                }
            });
        }

        public void MarkAsEnqueued()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsMarkedAsPathed())
                {
                    Background = EnqueuedVertexColor;
                }
            });
        }

        public void MakeUnweighted()
        {
            (cost as IWeightable)?.MakeUnweighted();
            Dispatcher.Invoke(() => Content = cost.ToString());
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

        private bool IsMarkedAsPathed()
        {
            return Background == AlreadyPathVertexColor 
                || Background == PathVertexColor 
                || Background == IntermediateVertexColor;
        }

        public void MarkAsIntermediate()
        {
            Dispatcher.Invoke(() => Background = IntermediateVertexColor);
        }
    }
}
