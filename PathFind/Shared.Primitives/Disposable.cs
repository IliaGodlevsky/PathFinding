using System;

namespace Shared.Primitives
{
    public readonly ref struct Disposable
    {
        private readonly Action action;

        public static Disposable Use(Action action) => new(action);

        private Disposable(Action action) => this.action = action;

        public readonly void Dispose() => action?.Invoke();
    }
}