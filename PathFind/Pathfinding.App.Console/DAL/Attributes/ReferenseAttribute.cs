using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class ReferenceAttribute : SqliteBuildAttribute
    {
        public ReferenceAttribute(string tableName, string referenceIdName)
            : base($"REFERENCES {tableName}({referenceIdName})", 4)
        {
        }
    }
}
