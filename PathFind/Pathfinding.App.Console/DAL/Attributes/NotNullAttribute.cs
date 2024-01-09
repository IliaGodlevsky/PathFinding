using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class NotNullAttribute : SqliteBuildAttribute
    {
        public NotNullAttribute() 
            : base("NOT NULL", 1)
        {
        }
    }
}
