using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Messages;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class MessengerExtensions
    {
        public static async Task SendAsync<T, TToken>(this IMessenger messenger, T message, TToken token)
            where T : class, IMayBeAsync
            where TToken : IEquatable<TToken>
        {
            var tcs = new TaskCompletionSource<Unit>();
            void Signal() => tcs.TrySetResult(Unit.Default);
            message.Signal = Signal;
            messenger.Send(message, token);
            await tcs.Task.ConfigureAwait(false);
        }
    }
}
