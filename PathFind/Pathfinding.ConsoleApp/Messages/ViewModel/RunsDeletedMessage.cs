using System;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class RunsDeletedMessage(int[] RunIds) : IMayBeAsync
    {
        public Action Signal { get; set; }
    }
}
