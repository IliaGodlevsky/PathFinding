namespace WPFVersion3D.Model
{
    internal sealed class Offset
    {
        public double GraphCenterOffset => AdjustedDimensionSize * AdjustedVertexSize;

        public double VertexOffset => AdjustedVertexSize * CoordinateValue + AdditionalOffset;

        public int CoordinateValue { private get; set; }

        public int DimensionSize { private get; set; }

        public double VertexSize { private get; set; }

        public double AdditionalOffset { private get; set; }

        public double DistanceBetweenVertices { private get; set; }

        private double AdjustedVertexSize => VertexSize + DistanceBetweenVertices;

        private double AdjustedDimensionSize => (-DimensionSize + AdditionalOffset) / 2;
    }
}
