using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Vertex.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Abstractions
{
    public abstract class BaseGraph : IGraph
    {
        public BaseGraph(params int[]dimensionSizes)
        {
            DimensionsSizes = dimensionSizes;

            vertices = new IVertex[Size];
            this.RemoveExtremeVertices();
        }

        public int Size => DimensionsSizes.Aggregate((x, y) => x * y);

        public int NumberOfVisitedVertices
            => vertices.AsParallel().Count(vertex => vertex.IsVisited);

        public int ObstacleNumber 
            => vertices.AsParallel().Count(vertex => vertex.IsObstacle);

        public int ObstaclePercent 
            => Size == 0 ? 0 : ObstacleNumber * 100 / Size;

        protected IVertex end;
        public virtual IVertex End
        {
            get => end;
            set { end = value; end.IsEnd = true; }
        }

        protected IVertex start;
        public virtual IVertex Start
        {
            get => start;
            set { start = value; start.IsStart = true; }
        }

        public VertexInfoCollection VertexInfoCollection 
            => new VertexInfoCollection(vertices, DimensionsSizes.ToArray());

        public IEnumerable<int> DimensionsSizes { get; private set; }

        public bool IsDefault => false;

        public virtual IVertex this[int index]
        {
            get => vertices[index];
            set => vertices[index] = value;
        }

        public virtual IEnumerator<IVertex> GetEnumerator()
        {
            return vertices.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return vertices.AsEnumerable().GetEnumerator();
        }

        public abstract string GetFormattedData(string format);

        public abstract IVertex this[ICoordinate coordinate] { get; set; }

        protected readonly IVertex[] vertices;
    }
}
