namespace Common
{
    public static class VertexParametres
    {
        static VertexParametres()
        {
            TextToSizeRatio = 0.475f;
            VertexSize = 24;
        }

        /// <summary>
        /// Ratio of the text size to vertex size
        /// </summary>
        public static float TextToSizeRatio { get; }

        private static int vertexSize;
        public static int VertexSize
        {
            get => vertexSize;
            set { vertexSize = value; SizeBetweenVertices = vertexSize + 1; }
        }

        public static int SizeBetweenVertices { get; private set; }
    }
}
