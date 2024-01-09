using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class IdentityAttribute : SqliteBuildAttribute
    {
        public IdentityAttribute() 
            : base("PRIMARY KEY", 2)
        {
        }
    }
}
