using GraphLib.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using GraphLib.VertexCost;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using static System.Configuration.ConfigurationManager;

namespace WindowsFormsVersion.Model
{
    internal class Vertex : Label, IVertex, IMarkableVertex, IWeightableVertex
    {
        public Vertex() : base()
        {
            float fontSizeRatio = float.Parse(AppSettings["textToSizeRatio"]);
            int vertexSize = Convert.ToInt32(AppSettings["vertexSize"]);
            float fontSize = vertexSize * fontSizeRatio;
            Font = new Font("Times New Roman", fontSize);
            Size = new Size(vertexSize, vertexSize);
            TextAlign = ContentAlignment.MiddleCenter;
            this.Initialize();
        }

        public Vertex(VertexSerializationInfo info) : this()
        {
            this.Initialize(info);
        }

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

        public void MarkAsSimpleVertex()
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
            (cost as Cost).MakeUnWeighted();
        }

        public void MakeWeighted()
        {
            (cost as Cost).MakeWeighted();
            Text = cost.ToString();
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

        public bool IsDefault => false;
    }
}
