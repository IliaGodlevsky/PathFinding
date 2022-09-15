using System;

namespace Common
{
    public sealed class Disposing : IDisposable
    {
        private readonly Action disposeAction;

        private Disposing(Action disposeAction)
        {
            this.disposeAction = disposeAction;
        }

        public static IDisposable Use(Action action)
        {
            return new Disposing(action);
        }

        void IDisposable.Dispose() => disposeAction?.Invoke();
    }
}