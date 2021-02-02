﻿using GraphLib.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using GraphLib.Vertex.Cost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
{
    internal class Vertex : Label, IVertex
    {
        public Vertex() : base()
        {
            float fontSizeRatio = float.Parse(ConfigurationManager.AppSettings["textToSizeRatio"]);
            int vertexSize = Convert.ToInt32(ConfigurationManager.AppSettings["vertexSize"]);
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

        public bool IsStart { get; set; }

        public bool IsEnd { get; set; }

        public bool IsVisited { get; set; }

        public double AccumulatedCost { get; set; }

        public IVertex ParentVertex { get; set; }

        public IList<IVertex> Neighbours { get; set; }

        public bool IsObstacle { get; set; }

        public void MarkAsObstacle()
        {
            BackColor = Color.FromKnownColor(KnownColor.Black);
            this.WashVertex();
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
            cost.MakeUnWeighted();
        }

        public void MakeWeighted()
        {
            cost.MakeWeighted();
            Text = cost.ToString();
        }

        private VertexCost cost;
        public VertexCost Cost
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
