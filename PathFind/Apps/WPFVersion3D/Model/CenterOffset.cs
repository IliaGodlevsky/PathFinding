namespace WPFVersion3D.Model
{
    internal readonly struct CenterOffset
    {
        public CenterOffset(int dimensionSize, 
            double vertexSize,
            double additionalOffset,
            double distanceBetweenVertices)
        {
            double adjustedDimensionSize = (additionalOffset - dimensionSize) / 2;
            double adjustedVertexSize = vertexSize + distanceBetweenVertices;
            this.centerOffset = adjustedDimensionSize * adjustedVertexSize;
        }

        public double GetCenterOffset()
        {
            return centerOffset;
        }

        private readonly double centerOffset;
    }
}
