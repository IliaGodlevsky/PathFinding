namespace WPFVersion3D.Model
{
    internal readonly struct GraphAxisCenteringOffset
    {
        public double Offset { get; }

        public GraphAxisCenteringOffset(int dimensionSize,
            double vertexSize,
            double additionalOffset,
            double distanceBetweenVertices)
        {
            double adjustedDimensionSize = (additionalOffset - dimensionSize) / 2;
            double adjustedVertexSize = vertexSize + distanceBetweenVertices;
            Offset = adjustedDimensionSize * adjustedVertexSize;
        }
    }
}
