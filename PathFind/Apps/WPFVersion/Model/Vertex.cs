using Common.Extensions;
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
    internal class Vertex : Label, IVertex, IVisualizable, IWeightable
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
                    VisualizeAsObstacle();
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

        public bool IsVisualizedAsPath => Background.IsOneOf(AlreadyPathVertexColor, PathVertexColor, IntermediateVertexColor);

        public bool IsVisualizedAsEndPoint => Background.IsOneOf(StartVertexColor, EndVertexColor, IntermediateVertexColor);

        public void VisualizeAsTarget()
        {
            Dispatcher.Invoke(() => Background = EndVertexColor);
        }

        public void VisualizeAsObstacle()
        {
            Dispatcher.Invoke(() => Background = ObstacleVertexColor);
        }

        public void VisualizeAsPath()
        {
            Dispatcher.Invoke(() =>
            {
                if (IsVisualizedAsPath)
                {
                    Background = AlreadyPathVertexColor;
                }
                else
                {
                    Background = PathVertexColor;
                }
            });
        }

        public void VisualizeAsSource()
        {
            Dispatcher.Invoke(() => Background = StartVertexColor);
        }

        public void VisualizeAsRegular()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsObstacle)
                {
                    Background = RegularVertexColor;
                }
            });
        }

        public void VisualizeAsVisited()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsVisualizedAsPath)
                {
                    Background = VisitedVertexColor;
                }
            });
        }

        public void VisualizeAsEnqueued()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsVisualizedAsPath)
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

        public void VisualizeAsIntermediate()
        {
            Dispatcher.Invoke(() => Background = IntermediateVertexColor);
        }
    }
}
