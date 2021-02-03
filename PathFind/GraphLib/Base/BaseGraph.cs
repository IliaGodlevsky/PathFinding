using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseGraph : IGraph
    {
        public BaseGraph(params int[] dimensionSizes)
        {
            DimensionsSizes = dimensionSizes.ToArray();

            vertices = new IVertex[this.GetSize()];
            this.RemoveExtremeVertices();
        }

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

        public virtual IVertex this[ICoordinate coordinate]
        {
            get
            {
                if (!coordinate.IsDefault)
                {
                    if (coordinate.CoordinatesValues.Count() != DimensionsSizes.Count())
                    {
                        throw new ArgumentException("Dimensions of graph and coordinate doesn't match");
                    }

                    return vertices[coordinate.ToIndex(this)];
                }

                return new DefaultVertex();
            }
            set
            {
                if (!coordinate.IsDefault)
                {
                    if (coordinate.CoordinatesValues.Count() != DimensionsSizes.Count())
                    {
                        throw new ArgumentException("Dimensions of graph and coordinate doesn't match");
                    }

                    vertices[coordinate.ToIndex(this)] = value;
                }
            }
        }

        protected readonly IVertex[] vertices;
    }
}
