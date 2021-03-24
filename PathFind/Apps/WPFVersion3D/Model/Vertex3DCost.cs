using GraphLib.Base;
using System;

namespace WPFVersion3D.Model
{
    [Serializable]
    internal sealed class Vertex3DCost : BaseVertexCost
    {
        public Vertex3DCost(int cost) : base(cost)
        {

        }

        public Vertex3DCost() : base()
        {

        }
    }
}
