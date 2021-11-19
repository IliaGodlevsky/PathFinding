using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.VertexCost;
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
    internal class Vertex : Label, IVertex, IVisualizable, IWeightable, IEquatable<IVertex>
    {
        private static Color RegularVertexColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
        private static Color ObstacleVertexColor = Color.FromKnownColor(KnownColor.Black);
        private static Color PathVertexColor = Color.FromKnownColor(KnownColor.Yellow);
        private static Color EnqueuedVertexColor = Color.FromKnownColor(KnownColor.Magenta);
        private static Color SourceVertexColor = Color.FromKnownColor(KnownColor.Green);
        private static Color TargetVertexColor = Color.FromKnownColor(KnownColor.Red);
        private static Color AlreadyPathVertexColor = Color.FromKnownColor(KnownColor.Gold);
        private static Color VisitedVertexColor = Color.FromKnownColor(KnownColor.CadetBlue);
        private static Color IntermediateVertexColor = Color.FromKnownColor(KnownColor.DarkOrange);
        private static Color ToReplaceMarkColor = Color.FromArgb(alpha: 185, red: 255, green: 140, blue: 0);

        public Vertex(INeighborhood coordinateRadar, ICoordinate coordinate) : base()
        {
            float fontSize = VertexSize * TextToSizeRatio;
            Font = new Font("Times New Roman", fontSize);
            Size = new Size(VertexSize, VertexSize);
            TextAlign = ContentAlignment.MiddleCenter;
            this.Initialize();
            Position = coordinate;
            Neighborhood = coordinateRadar;
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(this.GetNeighbours);
        }

        public Vertex(VertexSerializationInfo info)
            : this(info.NeighboursCoordinates, info.Position)
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
                Text = cost.ToString();
            }
        }

        public virtual INeighborhood Neighborhood { get; }

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

        public void VisualizeAsObstacle()
        {
            BackColor = ObstacleVertexColor;
        }

        public void VisualizeAsRegular()
        {
            BackColor = RegularVertexColor;
        }

        public void VisualizeAsSource()
        {
            BackColor = SourceVertexColor;
        }

        public void VisualizeAsTarget()
        {
            BackColor = TargetVertexColor;
        }

        public void VisualizeAsVisited()
        {
            if (!IsVisualizedAsPath)
            {
                BackColor = VisitedVertexColor;
            }
        }

        public void VisualizeAsPath()
        {
            if (IsVisualizedAsPath)
            {
                BackColor = AlreadyPathVertexColor;
            }
            else
            {
                BackColor = PathVertexColor;
            }
        }

        public void VisualizeAsEnqueued()
        {
            if (!IsVisualizedAsPath)
            {
                BackColor = EnqueuedVertexColor;
            }
        }

        public void MakeUnweighted()
        {
            Text = string.Empty;
            (cost as WeightableVertexCost)?.MakeUnweighted();
        }

        public void MakeWeighted()
        {
            (cost as WeightableVertexCost)?.MakeWeighted();
            Text = cost.ToString();
        }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        public bool IsVisualizedAsPath
            => BackColor.IsOneOf(PathVertexColor, AlreadyPathVertexColor, IntermediateVertexColor, ToReplaceMarkColor);

        public bool IsVisualizedAsEndPoint
            => BackColor.IsOneOf(SourceVertexColor, TargetVertexColor, IntermediateVertexColor, ToReplaceMarkColor);

        public void VisualizeAsIntermediate()
        {
            BackColor = IntermediateVertexColor;
        }

        public void VisualizeAsMarkedToReplaceIntermediate()
        {
            if (BackColor == IntermediateVertexColor)
            {
                BackColor = ToReplaceMarkColor;
            }
        }

        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;
    }
}
