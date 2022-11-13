using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Model
{
    internal class ConditionFailedException : Exception
    {
        public ConditionFailedException(string message) : base(message)
        {

        }
    }
}
