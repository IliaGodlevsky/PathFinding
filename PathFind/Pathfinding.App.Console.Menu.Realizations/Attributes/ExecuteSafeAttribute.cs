using System;

namespace Pathfinding.App.Console.Menu.Realizations.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class ExecuteSafeAttribute : MethodMarkAttribute
    {
        public ExecuteSafeAttribute(string methodName) : base(methodName)
        {

        }
    }
}
