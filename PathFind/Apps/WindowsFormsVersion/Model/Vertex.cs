using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static WindowsFormsVersion.Constants;

namespace WindowsFormsVersion.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex : Label, IVertex, IVisualizable
    {
        public Vertex(INeighborhood neighborhood, ICoordinate coordinate, IVisualization<Vertex> visualization) : base()
        {
            this.visualization = visualization;
            float fontSize = VertexSize * TextToSizeRatio;
            Font = new Font("Times New Roman", fontSize);
            Size = new Size(VertexSize, VertexSize);
            TextAlign = ContentAlignment.MiddleCenter;
            this.Initialize();
            Position = coordinate;
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(() => neighborhood.GetNeighboursWithinGraph(this));
        }

        public Vertex(VertexSerializationInfo info, IVisualization<Vertex> visualization)
            : this(info.Neighbourhood, info.Position, visualization)
        {
            this.Initialize(info);
        }

        public IGraph Graph { get; }

        private IVertexCost cost;
        public IVertexCost Cost
        {
            get => cost;
            set
            {
                cost = value;
                Text = cost.CurrentCost.ToString();
            }
        }

        public ICoordinate Position { get; }

        public IReadOnlyCollection<IVertex> Neighbours => neighbours.Value;

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

        public void VisualizeAsMarkedToReplaceIntermediate() => visualization.VisualizeAsMarkedToReplaceIntermediate(this);
        public void VisualizeAsObstacle() => visualization.VisualizeAsObstacle(this);
        public void VisualizeAsRegular() => visualization.VisualizeAsRegular(this);
        public void VisualizeAsSource() => visualization.VisualizeAsSource(this);
        public void VisualizeAsTarget() => visualization.VisualizeAsTarget(this);
        public void VisualizeAsVisited() => visualization.VisualizeAsVisited(this);
        public void VisualizeAsPath() => visualization.VisualizeAsPath(this);
        public void VisualizeAsEnqueued() => visualization.VisualizeAsEnqueued(this);
        public void VisualizeAsIntermediate() => visualization.VisualizeAsIntermediate(this);
        public bool IsVisualizedAsPath => visualization.IsVisualizedAsPath(this);
        public bool IsVisualizedAsEndPoint => visualization.IsVisualizedAsEndPoint(this);

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        public override bool Equals(object obj)
        {
            return obj is IVertex vertex && vertex.IsEqual(this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cost.CurrentCost, Position.GetHashCode());
        }

        private readonly IVisualization<Vertex> visualization;
        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;
    }
}