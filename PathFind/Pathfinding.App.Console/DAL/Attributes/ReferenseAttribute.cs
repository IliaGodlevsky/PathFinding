using Shared.Extensions;
using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class ReferenceAttribute(string tableName,
        string referenceIdName, string onDelete = ReferenceAttribute.OnDeleteNoAction) 
        : SqliteBuildAttribute(GetLine(tableName, referenceIdName, onDelete), 7)
    {
        public const string OnDeleteCascade = "CASCADE";
        public const string OnDeleteSetDefault = "SET DEFAULT";
        public const string OnDeleteNoAction = "NO ACTION";
        public const string OnDeleteSetNull = "SET NULL";

        private static string GetLine(string tableName, 
            string referenceIdName, string onDelete)
        {
            if (onDelete.IsOneOf(OnDeleteCascade, 
                OnDeleteSetDefault, OnDeleteNoAction, OnDeleteSetNull))
            {
                return $"REFERENCES {tableName}({referenceIdName}) ON DELETE {onDelete}";
            }
            throw new ArgumentException(nameof(onDelete));
        }
    }
}
