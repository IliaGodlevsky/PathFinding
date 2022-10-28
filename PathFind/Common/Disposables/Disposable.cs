using System;

namespace Common.Disposables
{
    public sealed class Disposable : IDisposable
    {
        public static IDisposable Use(Action action)
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

        void IDisposable.Dispose() => disposeAction();
    }
}