﻿using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base
{
    public abstract class BaseGraphFieldFactory : IGraphFieldFactory
    {
        protected abstract IGraphField GetField();

        public virtual IGraphField CreateGraphField(IGraph graph)
        {
            var graphField = GetField();
            graph.ForEach(graphField.Add);

            return graphField;
        }
    }
}
