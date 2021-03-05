using GraphLib.Extensions;
using GraphLib.Interface;
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
        public Vertex() : base()
        {
            float fontSize = VertexSize * TextToSizeRatio;
            Font = new Font("Times New Roman", fontSize);
            Size = new Size(VertexSize, VertexSize);
            TextAlign = ContentAlignment.MiddleCenter;
            this.Initialize();
        }

        public Vertex(VertexSerializationInfo info) : this()
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

        public ICoordinate Position { get; set; }

        public IList<IVertex> Neighbours { get; set; }

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

        public void MarkAsObstacle()
        {
            BackColor = Color.FromKnownColor(KnownColor.Black);
        }

        public void MarkAsRegular()
        {
            if (!IsObstacle)
            {
                BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
            }
        }

        public void MarkAsStart()
        {
            BackColor = Color.FromKnownColor(KnownColor.Green);
        }

        public void MarkAsEnd()
        {
            BackColor = Color.FromKnownColor(KnownColor.Red);
        }

        public void MarkAsVisited()
        {
            BackColor = Color.FromKnownColor(KnownColor.CadetBlue);
        }

        public void MarkAsPath()
        {
            BackColor = Color.FromKnownColor(KnownColor.Yellow);
        }

        public void MarkAsEnqueued()
        {
            BackColor = Color.FromKnownColor(KnownColor.Magenta);
        }

        public void MakeUnweighted()
        {
            Text = string.Empty;
            (cost as WeightableVertexCost).MakeUnweighted();
        }

        public void MakeWeighted()
        {
            (cost as WeightableVertexCost).MakeWeighted();
            Text = cost.ToString();
        }
    }
}
