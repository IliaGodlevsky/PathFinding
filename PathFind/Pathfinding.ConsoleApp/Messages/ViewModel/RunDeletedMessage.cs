using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal class RunsDeletedMessage
    {
        public int[] RunIds { get; }

        public RunsDeletedMessage(int[] runIds)
        {
            RunIds = runIds;
        }
    }
}
