namespace WPFVersion3D.Model
{
    internal readonly struct LocationOffset
    {
        public LocationOffset(int coordinateValue, 
            double distanceBetweenVertices, 
            double additionalOffset, 
            double vertexSize)
        {
            double adjustedVertexSize = vertexSize + distanceBetweenVertices;
            this.locationOffset = (adjustedVertexSize * coordinateValue) + additionalOffset;
        }

        public double GetLocationOffset()
        {
            return locationOffset;
        }

        private readonly double locationOffset;
    }
}
