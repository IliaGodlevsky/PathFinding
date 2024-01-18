using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class UniqueAttribute : SqliteBuildAttribute
    {
        public UniqueAttribute() 
            : base("UNIQUE", 5)
        {
        }
    }
}
