using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex : ContentControl, IVertex, IVisualizable, IWeightable, IEquatable<IVertex>
    {
        public Vertex(INeighborhood neighborhood, ICoordinate coordinate, IVisualization<Vertex> visualization) : base()
        {
            this.visualization = visualization;
            Width = Height = VertexSize;
            Template = (ControlTemplate)TryFindResource("vertexTemplate");
            Position = coordinate;
            this.Initialize();
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(() => neighborhood.GetNeighbours(this));
        }

        public Vertex(VertexSerializationInfo info, IVisualization<Vertex> visualization)
            : this(info.Neighbourhood, info.Position, visualization)
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

        public virtual INeighborhood Neighborhood { get; }

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

        public IReadOnlyCollection<IVertex> Neighbours => neighbours.Value;

        public IGraph Graph { get; }

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

        public bool Equals(IVertex other) => other.IsEqual(this);
        public bool IsVisualizedAsPath => visualization.IsVisualizedAsPath(this);
        public bool IsVisualizedAsEndPoint => visualization.IsVisualizedAsEndPoint(this);
        public void VisualizeAsTarget() => visualization.VisualizeAsTarget(this);
        public void VisualizeAsObstacle() => visualization.VisualizeAsObstacle(this);
        public void VisualizeAsPath() => visualization.VisualizeAsPath(this);
        public void VisualizeAsSource() => visualization.VisualizeAsSource(this);
        public void VisualizeAsRegular() => visualization.VisualizeAsRegular(this);
        public void VisualizeAsVisited() => visualization.VisualizeAsVisited(this);
        public void VisualizeAsEnqueued() => visualization.VisualizeAsEnqueued(this);
        public void VisualizeAsIntermediate() => visualization.VisualizeAsIntermediate(this);
        public void VisualizeAsMarkedToReplaceIntermediate() => visualization.VisualizeAsMarkedToReplaceIntermediate(this);

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

        private readonly IVisualization<Vertex> visualization;
        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;
    }
}