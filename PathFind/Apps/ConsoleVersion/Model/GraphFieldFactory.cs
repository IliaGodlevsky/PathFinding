using ConsoleVersion.View;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Realizations;
using System;

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
            if (!(graph is Graph2D graph2d))
            {
                throw new ArgumentException(nameof(graph));
            }

            if (!(base.CreateGraphField(graph) is GraphField field))
            {
                string message = $"{nameof(field)} is not of type {nameof(GraphField)}";
                throw new Exception(message);
            }

            field.Width = graph2d.Width;
            field.Length = graph2d.Length;

            return field;
        }
    }
}
