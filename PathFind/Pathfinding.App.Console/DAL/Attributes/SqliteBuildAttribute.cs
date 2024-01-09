using Shared.Primitives.Attributes;
using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    internal abstract class SqliteBuildAttribute : OrderAttribute
    {
        public string Line { get; }

        protected SqliteBuildAttribute(string line, int order)
            : base(order)
        {
            Line = line;
        }
    }
}
