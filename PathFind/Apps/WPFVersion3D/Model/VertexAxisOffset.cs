namespace WPFVersion3D.Model
{
    internal readonly struct VertexAxisOffset
    {
        public double Offset { get; }

        public VertexAxisOffset(int coordinateValue,
            double distanceBetweenVertices,
            double additionalOffset,
            double vertexSize)
        {
            double adjustedVertexSize = vertexSize + distanceBetweenVertices;
            Offset = (adjustedVertexSize * coordinateValue) + additionalOffset;
        }
    }
}
