﻿using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Windows.Input;

namespace WPFVersion.Model
{
    internal sealed class VertexEventHolder : BaseVertexEventHolder
    {
        public VertexEventHolder(IVertexCostFactory costFactory) : base(costFactory)
        {
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return e is MouseWheelEventArgs args ? args.Delta : default;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseRightButtonDown += Reverse;
                vert.MouseWheel += ChangeVertexCost;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseRightButtonDown -= Reverse;
                vert.MouseWheel -= ChangeVertexCost;
            }
        }
    }
}
