﻿using GraphLibrary.Extensions;
using GraphLibrary.Extensions.RandomExtension;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.RoleChanger
{
    public abstract class AbstractVertexRoleChanger : IVertexRoleChanger
    {
        protected AbstractGraph graph;

        public AbstractVertexRoleChanger(AbstractGraph graph)
        {
            this.graph = graph;
        }

        public abstract void ChangeTopText(object sender, EventArgs e);

        private void MakeObstacle(ref IVertex top)
        {
            if (top.IsSimpleVertex)
            {
                BoundSetter.BreakBoundsBetweenNeighbours(top);
                top.IsObstacle = false;
                top.SetToDefault();
                top.MarkAsObstacle();
            }
        }

        private void MakeTop(ref IVertex top)
        {
            var rand = new Random();
            top.IsObstacle = false;
            top.MarkAsSimpleVertex();
            top.Text = rand.GetRandomVertexValue();
            var setter = new NeigbourSetter(graph);
            var coordinates = graph.GetIndices(top);
            setter.SetNeighbours(coordinates.X, coordinates.Y);
            BoundSetter.SetBoundsBetweenNeighbours(top);
        }

        protected void Reverse(ref IVertex top)
        {
            if (top.IsObstacle)
                MakeTop(ref top);
            else
                MakeObstacle(ref top);
        }

        public virtual void SetStartPoint(object sender, EventArgs e)
        {
            var top = sender as IVertex;
            if (!top.IsValidToBeRange())
                return;
            top.IsStart = true;
            top.MarkAsStart();
            graph.Start = top;
        }

        public virtual void SetDestinationPoint(object sender, EventArgs e)
        {
            var top = sender as IVertex;
            if (!top.IsValidToBeRange())
                return;
            top.IsEnd = true;
            top.MarkAsEnd();
            graph.End = top;
        }

        public virtual void ReversePolarity(object sender, EventArgs e)
        {
            IVertex top = sender as IVertex;
            Reverse(ref top);
        }
    }
}