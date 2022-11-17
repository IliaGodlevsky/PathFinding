using System;

namespace Pathfinding.App.Console.Model.Menu.Exceptions
{
    internal class ConditionFailedException : Exception
    {
        public ConditionFailedException(string message)
            : base(message)
        {

        }
    }
}
