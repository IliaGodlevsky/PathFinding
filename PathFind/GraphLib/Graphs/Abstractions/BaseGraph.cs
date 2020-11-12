using GraphLib.Coordinates.Interface;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface;
using GraphLib.Vertex.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Abstractions
{
    public abstract class BaseGraph : IGraph
    {
        public abstract IVertex this[ICoordinate coordinate] { get; set; }

        public virtual int Size => DimensionsSizes.Aggregate((x, y) => x * y);

        public virtual int NumberOfVisitedVertices => vertices.AsParallel().Count(vertex => vertex.IsVisited);

        public virtual int ObstacleNumber => vertices.AsParallel().Count(vertex => vertex.IsObstacle);

        public virtual int ObstaclePercent => Size == 0 ? 0 : ObstacleNumber * 100 / Size;

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

        public abstract IVertexInfoCollection VertexInfoCollection { get; }

        public abstract IEnumerable<int> DimensionsSizes { get; }

        public abstract bool IsDefault { get; }

        public virtual IEnumerator<IVertex> GetEnumerator()
        {
            return vertices.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return vertices.AsEnumerable().GetEnumerator();
        }

        public abstract string GetFormattedData(string format);

        protected IVertex[] vertices;
    }
}
