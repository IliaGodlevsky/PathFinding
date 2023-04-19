using System;
using System.Threading.Tasks;

namespace Shared.Primitives
{
    public readonly ref struct Disposable
    {
        private readonly Action[] actions;

        public static Disposable Use(params Action[] actions)
        {
            return new(actions);
        }

        private Disposable(Action[] actions)
        {
            this.actions = actions;
        }

        public readonly void Dispose()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i]?.Invoke();
            }
        }
    }
}