using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Messages;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class MessengerExtensions
    {
        public static async Task SendAsync<TMessage, TToken>(this IMessenger messenger,
            TMessage message, TToken token)
            where TMessage : class, IAsyncMessage<Unit>
            where TToken : IEquatable<TToken>
        {
            var tcs = new TaskCompletionSource<Unit>();
            void Signal(Unit unit) => tcs.TrySetResult(unit);
            message.Signal = Signal;
            var timeout = Task.Delay(TimeSpan.FromSeconds(100));
            messenger.Send(message, token);
            await Task.WhenAny(timeout, tcs.Task).ConfigureAwait(false);
        }
    }
}
