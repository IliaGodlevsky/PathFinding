using Common.Extensions;
using System.Linq;
using Wpf3dVersion.Enums;
using OffsetCallback = System.Action<double, Wpf3dVersion.Model.WpfGraphField3D>;

namespace Wpf3dVersion.Model
{
    internal class DistanceBetweenVertices
    {
        public DistanceBetweenVertices(Axis axis, WpfGraphField3D field,
            double distanceBetween, params double[] additionalOffset)
        {
            this.axis = axis;
            this.additionalOffset = additionalOffset.ToArray();
            this.field = field;
            this.distanceBetween = distanceBetween;
        }

        public void SetDistance()
        {
            int axisIndex = axis.GetValue();
            CallBacks[axisIndex](distanceBetween, field);
            field.SetDistanceBetweenVertices();

            if (distanceBetween == 0)
                field.CenterGraph(0, 0, 0);
            else
                field.CenterGraph(additionalOffset);
        }

        private OffsetCallback[] CallBacks => new OffsetCallback[]
        {
            (distanceBetween, field) => field.DistanceBetweenVerticesAtXAxis = distanceBetween,
            (distanceBetween, field) => field.DistanceBetweenVerticesAtYAxis = distanceBetween,
            (distanceBetween, field) => field.DistanceBetweenVerticesAtZAxis = distanceBetween
        };

        private readonly double distanceBetween;
        private readonly WpfGraphField3D field;
        private readonly Axis axis;
        private readonly double[] additionalOffset;
    }
}
