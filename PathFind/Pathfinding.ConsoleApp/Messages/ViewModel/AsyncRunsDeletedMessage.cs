using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Reactive;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class AsyncRunsDeletedMessage : IAsyncMessage<Unit>
    {
        public int[] RunIds { get; }

        public Action<Unit> Signal { get; set; } = unit => throw new InvalidOperationException();

        public AsyncRunsDeletedMessage(int[] runIds)
        {
            RunIds = runIds;
        }
    }
}
