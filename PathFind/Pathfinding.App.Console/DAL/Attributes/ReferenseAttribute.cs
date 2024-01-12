using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    internal enum OnDelete
    {
        NoAction,
        Cascade,
        SetNull,
        SetDefault
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class ReferenceAttribute : SqliteBuildAttribute
    {
        public ReferenceAttribute(string tableName, 
            string referenceIdName, OnDelete onDelete = OnDelete.NoAction)
            : base(GetLine(tableName, referenceIdName, onDelete), 6)
        {
        }

        private static string GetLine(string tableName, 
            string referenceIdName, OnDelete onDelete )
        {
            return $"REFERENCES {tableName}({referenceIdName}) " +
                $"ON DELETE {GetOnDeleteStatement(onDelete)}";
        }

        private static string GetOnDeleteStatement(OnDelete onDelete)
        {
            return onDelete switch
            {
                OnDelete.Cascade => "CASCADE",
                OnDelete.SetDefault => "SET DEFAULT",
                OnDelete.NoAction => "NO ACTION",
                OnDelete.SetNull => "SET NULL",
                _ => throw new ArgumentOutOfRangeException(nameof(onDelete))
            };
        }
    }
}
