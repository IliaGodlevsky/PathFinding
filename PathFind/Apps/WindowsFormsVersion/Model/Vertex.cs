using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.VertexCost;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static WindowsFormsVersion.Constants;

namespace WindowsFormsVersion.Model
{
    internal class Vertex : Label, IVertex, IMarkable, IWeightable
    {
        private static Color RegularVertexColor;
        private static Color ObstacleVertexColor;
        private static Color PathVertexColor;
        private static Color EnqueuedVertexColor;
        private static Color SourceVertexColor;
        private static Color TargetVertexColor;
        private static Color AlreadyPathVertexColor;
        private static Color VisitedVertexColor;

        static Vertex()
        {
            RegularVertexColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
            ObstacleVertexColor = Color.FromKnownColor(KnownColor.Black);
            PathVertexColor = Color.FromKnownColor(KnownColor.Yellow);
            EnqueuedVertexColor = Color.FromKnownColor(KnownColor.Magenta);
            SourceVertexColor = Color.FromKnownColor(KnownColor.Green);
            TargetVertexColor = Color.FromKnownColor(KnownColor.Red);
            AlreadyPathVertexColor = Color.FromKnownColor(KnownColor.Gold);
            VisitedVertexColor = Color.FromKnownColor(KnownColor.CadetBlue);
        }

        public Vertex(INeighboursCoordinates coordinateRadar, ICoordinate coordinate) : base()
        {
            float fontSize = VertexSize * TextToSizeRatio;
            Font = new Font("Times New Roman", fontSize);
            Size = new Size(VertexSize, VertexSize);
            TextAlign = ContentAlignment.MiddleCenter;
            this.Initialize();
            Position = coordinate;
            NeighboursCoordinates = coordinateRadar;
        }

        public Vertex(VertexSerializationInfo info)
            : this(info.NeighboursCoordinates, info.Position)
        {
            this.Initialize(info);
        }

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

        public virtual INeighboursCoordinates NeighboursCoordinates { get; }

        public ICoordinate Position { get; }

        public ICollection<IVertex> Neighbours { get; set; }

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

        public void MarkAsObstacle()
        {
            BackColor = ObstacleVertexColor;
        }

        public void MarkAsRegular()
        {
            if (!IsObstacle)
            {
                BackColor = RegularVertexColor;
            }
        }

        public void MarkAsSource()
        {
            BackColor = SourceVertexColor;
        }

        public void MarkAsTarget()
        {
            BackColor = TargetVertexColor;
        }

        public void MarkAsVisited()
        {
            if (!IsMarkedAsPath())
            {
                BackColor = VisitedVertexColor;
            }
        }

        public void MarkAsPath()
        {
            if (IsMarkedAsPath())
            {
                BackColor = AlreadyPathVertexColor;
            }
            else
            {
                BackColor = PathVertexColor;
            }            
        }

        public void MarkAsEnqueued()
        {
            if (!IsMarkedAsPath())
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

        private bool IsMarkedAsPath()
        {
            return BackColor == PathVertexColor || BackColor == AlreadyPathVertexColor;
        }
    }
}
