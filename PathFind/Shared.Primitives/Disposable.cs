using System;
using System.Threading.Tasks;

namespace Shared.Primitives
{
    public sealed class Disposable : IDisposable, IAsyncDisposable
    {
        private readonly Action[] actions;

        public static IDisposable Use(params Action[] action) => new Disposable(action);

        public static IAsyncDisposable UseAsync(params Action[] action) => new Disposable(action);

        private Disposable(Action[] actions)
        {
            this.actions = actions ?? Array.Empty<Action>();
        }

        private static void Invoke(Action action) => action?.Invoke();

        private static void Invoke(Action[] actions) => Array.ForEach(actions, Invoke);

        void IDisposable.Dispose() => Invoke(actions);

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await Task.Run(() => Invoke(actions)).ConfigureAwait(false);
        }
    }
}