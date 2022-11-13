using System;

namespace Pathfinding.App.Console.Model.MenuCommands.Attributes
{
    internal abstract class MethodMarkAttribute : Attribute, IMethodMark
    {
        public string MethodName { get; }

        protected MethodMarkAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
