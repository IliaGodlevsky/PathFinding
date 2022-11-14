using System;

namespace Pathfinding.App.Console.Menu.Realizations.Exceptions
{
    internal class ConditionFailedException : Exception
    {
        public ConditionFailedException(string message) 
            : base(message)
        {

        }
    }
}
