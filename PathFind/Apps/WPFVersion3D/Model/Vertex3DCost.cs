using Common.Interface;
using GraphLib.Interfaces;
using System;

namespace WPFVersion3D.Model
{
    [Serializable]
    internal sealed class Vertex3DCost : IVertexCost, ICloneable<IVertexCost>
    {
        public int CurrentCost { get; } = 1;

        public override bool Equals(object obj)
        {
            return obj is IVertexCost cost && cost.CurrentCost == CurrentCost;
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
        }

        public IVertexCost Clone()
        {
            return new Vertex3DCost();
        }
    }
}
