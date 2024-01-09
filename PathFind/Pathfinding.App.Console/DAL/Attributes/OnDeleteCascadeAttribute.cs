using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class OnDeleteCascadeAttribute : SqliteBuildAttribute
    {
        public OnDeleteCascadeAttribute() 
            : base("ON DELETE CASCADE", 5)
        {
            
        }
    }
}
