using System;
using System.Threading.Tasks;

namespace Common.Disposables
{
    public sealed class Disposable : IDisposable, IAsyncDisposable
    {
        public static IDisposable Use(Action action) => GetDisposable(action);

        public static IAsyncDisposable UseAsync(Action action) => GetDisposable(action);

        private static Disposable GetDisposable(Action action)
        {
            return action == null
                ? throw new ArgumentNullException(nameof(action))
                : new Disposable(action);
        }

        private readonly Action disposeAction;

        private Disposable(Action disposeAction)
        {
            this.disposeAction = disposeAction;
        }

        async ValueTask IAsyncDisposable.DisposeAsync() => await Task.Run(disposeAction);

        void IDisposable.Dispose() => disposeAction();
    }
}