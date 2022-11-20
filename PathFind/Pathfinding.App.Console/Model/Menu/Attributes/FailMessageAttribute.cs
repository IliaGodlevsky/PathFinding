using System;

namespace Pathfinding.App.Console.Model.Menu.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class FailMessageAttribute : Attribute
    {
        public static readonly FailMessageAttribute Default
            = new FailMessageAttribute("You did something wrong");

        public string Message { get; }

        public FailMessageAttribute(string message)
        {
            Message = message;
        }
    }
}
