using System;

namespace Common
{
    public sealed class Disposing : IDisposable
    {
        private readonly Action disposeAction;

        public Disposing(Action disposeAction)
        {
            this.disposeAction = disposeAction;
        }

        public void Dispose() => disposeAction?.Invoke();
    }
}