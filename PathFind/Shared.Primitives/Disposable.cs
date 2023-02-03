using System;

namespace Shared.Primitives
{
    public sealed class Disposable : IDisposable
    {
        private readonly Action[] actions;

        public static IDisposable Use(params Action[] actions)
        {
            return new Disposable(actions);
        }

        private Disposable(Action[] actions)
        {
            this.actions = actions;
        }

        void IDisposable.Dispose()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i]?.Invoke();
            }
        }
    }
}