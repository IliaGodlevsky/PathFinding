using ConsoleVersion.View;
using GraphLib.Base;
using GraphLib.Interfaces;
using System;
using System.Linq;

namespace ConsoleVersion.Model
{
    internal sealed class GraphFieldFactory : BaseGraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new GraphField();
        }

        /// <summary>
        /// Creates graph field from <paramref name="graph"/>
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>Graph field</returns>
        public override IGraphField CreateGraphField(IGraph graph)
        {
            if (!(graph is IGraph))
            {
                string message = $"{nameof(graph)} is not of type {nameof(IGraph)}";
                throw new ArgumentException(nameof(graph));
            }

            if (!(base.CreateGraphField(graph) is GraphField field))
            {
                string message = $"{nameof(field)} is not of type {nameof(GraphField)}";
                throw new Exception(message);
            }

            field.Width = graph.DimensionsSizes.ElementAtOrDefault(0);
            field.Length = graph.DimensionsSizes.ElementAtOrDefault(1);

            return field;
        }
    }
}
