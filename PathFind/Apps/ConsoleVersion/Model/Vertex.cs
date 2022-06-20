using ConsoleVersion.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using Console = Colorful.Console;

namespace ConsoleVersion.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex : IVertex, IVisualizable, IDisplayable
    {
        public event EventHandler VertexCostChanged;
        public event EventHandler EndPointChosen;
        public event EventHandler VertexReversed;
        public event EventHandler MarkedToReplaceIntermediate;

        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;
        private readonly IVisualization<Vertex> visualization;

        private bool isObstacle;
        private IVertexCost cost;

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

        public IVertexCost Cost
        {
            get => cost;
            set
            {
                cost = value;
                Text = cost.CurrentCost.ToString();
            }
        }

        public IGraph Graph { get; }

        public Coordinate2D ConsolePosition { get; set; }

        public Color Color { get; set; }

        public IReadOnlyCollection<IVertex> Neighbours => neighbours.Value;

        public ICoordinate Position { get; }

        public bool IsVisualizedAsPath => visualization.IsVisualizedAsPath(this);

        public bool IsVisualizedAsEndPoint => visualization.IsVisualizedAsEndPoint(this);

        private string Text { get; set; }

        public Vertex(INeighborhood neighbourhood, ICoordinate coordinate, IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
            Position = coordinate;
            this.Initialize();
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(() => neighbourhood.GetNeighboursWithinGraph(this));
        }

        public Vertex(VertexSerializationInfo info, IVisualization<Vertex> visualization)
            : this(info.Neighbourhood, info.Position, visualization)
        {
            this.Initialize(info);
        }

        public void ChangeCost()
        {
            VertexCostChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Reverse()
        {
            VertexReversed?.Invoke(this, EventArgs.Empty);
        }

        public void OnEndPointChosen()
        {
            EndPointChosen?.Invoke(this, EventArgs.Empty);
        }

        public void OnMarkedToReplaceIntermediate()
        {
            MarkedToReplaceIntermediate?.Invoke(this, EventArgs.Empty);
        }

        public void Display()
        {
            if (ConsolePosition != null)
            {
                Cursor.SetPosition(ConsolePosition);
                Console.Write(Text, Color);
            }
        }

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

        public void VisualizeAsTarget()
        {
            visualization.VisualizeAsTarget(this);
        }

        public void VisualizeAsRegular()
        {
            visualization.VisualizeAsRegular(this);
        }

        public void VisualizeAsObstacle()
        {
            visualization.VisualizeAsObstacle(this);
        }

        public void VisualizeAsPath()
        {
            visualization.VisualizeAsPath(this);
        }

        public void VisualizeAsSource()
        {
            visualization.VisualizeAsSource(this);
        }

        public void VisualizeAsVisited()
        {
            visualization.VisualizeAsVisited(this);
        }

        public void VisualizeAsEnqueued()
        {
            visualization.VisualizeAsEnqueued(this);
        }

        public void VisualizeAsIntermediate()
        {
            visualization.VisualizeAsIntermediate(this);
        }

        public void VisualizeAsMarkedToReplaceIntermediate()
        {
            visualization.VisualizeAsMarkedToReplaceIntermediate(this);
        }
    }
}