using System;

namespace Common.Disposables
{
    public sealed class Disposable : IDisposable
    {
        private readonly Action disposeAction;

        private Disposable(Action disposeAction)
        {
            this.disposeAction = disposeAction;
        }

        public static IDisposable Use(Action action)
        {
            return action == null
                ? throw new ArgumentNullException(nameof(action))
                : new Disposable(action);
        }

        void IDisposable.Dispose() => disposeAction();
    }
}