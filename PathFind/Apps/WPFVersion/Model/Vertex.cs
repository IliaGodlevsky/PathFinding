using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal class Vertex : Label, IVertex, IVisualizable, IWeightable
    {
        public static SolidColorBrush VisitedVertexColor { get; set; } = new SolidColorBrush(Colors.CadetBlue);
        public static SolidColorBrush PathVertexColor { get; set; } = new SolidColorBrush(Colors.Yellow);
        public static SolidColorBrush StartVertexColor { get; set; } = new SolidColorBrush(Colors.Green);
        public static SolidColorBrush EndVertexColor { get; set; } = new SolidColorBrush(Colors.Red);
        public static SolidColorBrush EnqueuedVertexColor { get; set; } = new SolidColorBrush(Colors.Magenta);
        public static SolidColorBrush ObstacleVertexColor { get; set; } = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush RegularVertexColor { get; set; } = new SolidColorBrush(Colors.White);
        public static SolidColorBrush AlreadyPathVertexColor { get; set; } = new SolidColorBrush(Colors.Gold);
        public static SolidColorBrush IntermediateVertexColor { get; set; } = new SolidColorBrush(Colors.DarkOrange);
        public static SolidColorBrush ToReplaceMarkColor { get; set; } = new SolidColorBrush(new Color { A = 185, B = 0, R = 255, G = 140 });

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

        public bool IsVisualizedAsPath
            => Background.IsOneOf(AlreadyPathVertexColor, PathVertexColor, IntermediateVertexColor, ToReplaceMarkColor);

        public bool IsVisualizedAsEndPoint
            => Background.IsOneOf(StartVertexColor, EndVertexColor, IntermediateVertexColor, ToReplaceMarkColor);

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

        public void VisualizeAsMarkedToReplaceIntermediate()
        {
            if (Background == IntermediateVertexColor)
            {
                Dispatcher.Invoke(() => Background = ToReplaceMarkColor);
            }
        }

        public IVertex Clone()
        {
            var neighbourCoordinates = NeighboursCoordinates.Clone();
            var coordinates = Position.Clone();
            var vertex = new Vertex(neighbourCoordinates, coordinates)
            {
                IsObstacle = IsObstacle,
                Cost = Cost.Clone(),
                Neighbours = Neighbours.Select(v => v.Clone()).ToArray()
            };
            return vertex;
        }
    }
}
