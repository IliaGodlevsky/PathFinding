using Pathfinding.App.Console.DAL.Interface;
using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    internal abstract class SqliteBuildAttribute : Attribute, ISqliteBuildAttribute
    {
        public int Order { get; }

        public string Text { get; }

        protected SqliteBuildAttribute(string text, int order)
        {
            Text = text;
            Order = order;
        }
    }
}
