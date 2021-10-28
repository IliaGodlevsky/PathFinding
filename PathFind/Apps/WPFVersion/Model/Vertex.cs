using Common.Interface;
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
    [DebuggerDisplay("{Position.ToString()})")]
    internal class Vertex : ContentControl, IVertex, IVisualizable, IWeightable, IEquatable<IVertex>, ICloneable<IVertex>
    {
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

        public IGraph Graph { get; }

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

        public bool Equals(IVertex other) => other.IsEqual(this);
        public bool IsVisualizedAsPath => ColorsHub.IsVisualizedAsPath(this);
        public bool IsVisualizedAsEndPoint => ColorsHub.IsVisualizedAsEndPoints(this);
        public void VisualizeAsTarget() => ColorsHub.VisualizeAsTarget(this);
        public void VisualizeAsObstacle() => ColorsHub.VisualizeAsObstacle(this);
        public void VisualizeAsPath() => ColorsHub.VisualizeAsPath(this);
        public void VisualizeAsSource() => ColorsHub.VisualizeAsSource(this);
        public void VisualizeAsRegular() => ColorsHub.VisualizeAsRegular(this);
        public void VisualizeAsVisited() => ColorsHub.VisualizeAsVisited(this);
        public void VisualizeAsEnqueued() => ColorsHub.VisualizeAsEnqueued(this);
        public void VisualizeAsIntermediate() => ColorsHub.VisualizeAsIntermediate(this);
        public void VisualizeAsMarkedToReplaceIntermediate() => ColorsHub.VisualizeAsMarkedToReplaceIntermediate(this);

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

        public IVertex Clone()
        {
            var neighbourCoordinates = NeighboursCoordinates.Clone();
            var coordinates = Position.Clone();
            var vertex = new Vertex(neighbourCoordinates, coordinates);
            return vertex.CloneProperties(this);
        }

        private static readonly VerticesColorsHub ColorsHub = new VerticesColorsHub();
    }
}