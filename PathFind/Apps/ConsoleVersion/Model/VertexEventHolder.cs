﻿using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;

using static ConsoleVersion.Resource.Resources;
using static GraphLib.Base.BaseVertexCost;

namespace ConsoleVersion.Model
{
    internal sealed class VertexEventHolder : BaseVertexEventHolder, IVertexEventHolder
    {
        public VertexEventHolder(IVertexCostFactory costFactory)
            : base(costFactory)
        {

        }

        public override void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is Vertex vertex)
            {
                if (!vertex.IsObstacle)
                {
                    var cost = Program.Input.InputNumber(VertexCostInputMsg, CostRange);
                    vertex.Cost = costFactory.CreateCost(cost);
                }
            }
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.OnVertexCostChanged += ChangeVertexCost;
                vert.OnVertexReversed += Reverse;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.OnVertexCostChanged -= ChangeVertexCost;
                vert.OnVertexReversed -= Reverse;
            }
        }
    }
}
