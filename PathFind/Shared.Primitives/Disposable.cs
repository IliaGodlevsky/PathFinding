using System;
using System.Threading.Tasks;

namespace Shared.Primitives
{
    public sealed class Disposable : IDisposable, IAsyncDisposable
    {
        private readonly Action[] actions;

        public static Disposable Use(params Action[] actions) => new(actions);

        private Disposable(Action[] actions) => this.actions = actions;

        private void Invoke() => Array.ForEach(actions, a => a?.Invoke());

        void IDisposable.Dispose() => Invoke();

        ValueTask IAsyncDisposable.DisposeAsync() => new(Task.Run(Invoke));
    }
}