using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class AutoincrementAttribute : SqliteBuildAttribute
    {
        public AutoincrementAttribute() 
            : base("AUTOINCREMENT", 3)
        {
        }
    }
}
