using System;

namespace Pathfinding.App.Console.Model.MenuCommands.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class ExecuteSafeAttribute : MethodMarkAttribute
    {
        public ExecuteSafeAttribute(string methodName) : base(methodName)
        {

        }
    }
}
