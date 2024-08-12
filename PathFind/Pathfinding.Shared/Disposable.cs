using System;

namespace Pathfinding.Shared
{
    public readonly ref struct Disposable
    {
        private readonly Action action;

        public static Disposable Use(Action action)
            => new(action ?? throw new ArgumentNullException(nameof(action)));

        private Disposable(Action action) => this.action = action;

        public readonly void Dispose() => action.Invoke();
    }
}