using System;

namespace Shared.Primitives
{
    public sealed class DisposableClass : IDisposable
    {
        private readonly Action action;

        public static IDisposable Use(Action action)
            => new DisposableClass(action ?? throw new ArgumentNullException(nameof(action)));

        private DisposableClass(Action action) => this.action = action;

        public void Dispose() => action.Invoke();
    }

    public readonly ref struct Disposable
    {
        private readonly Action action;

        public static Disposable Use(Action action)
            => new(action ?? throw new ArgumentNullException(nameof(action)));

        private Disposable(Action action) => this.action = action;

        public readonly void Dispose() => action.Invoke();
    }
}