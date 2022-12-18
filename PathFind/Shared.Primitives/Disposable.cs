using System;
using System.Threading.Tasks;

namespace Shared.Primitives
{
    /// <summary>
    /// An object, that can contain 
    /// an array of <see cref="Action"/>s 
    /// that are invoked when this object 
    /// is being disposed
    /// </summary>
    public sealed class Disposable : IDisposable, IAsyncDisposable
    {
        private readonly Action[] actions;

        /// <summary>
        /// Uses <paramref name="actions"/> that 
        /// will be invoked after <see cref="IDisposable"/> 
        /// is disposed
        /// </summary>
        /// <param name="actions"></param>
        /// <returns>An object, that invokes
        /// <paramref name="actions"/>
        /// during the disposing</returns>
        public static IDisposable Use(params Action[] actions)
        {
            return new Disposable(actions);
        }

        public static IAsyncDisposable UseAsync(params Action[] actions)
        {
            return new Disposable(actions);
        }

        private Disposable(Action[] actions)
        {
            this.actions = actions ?? Array.Empty<Action>();
        }

        private static void Invoke(Action action)
        {
            action?.Invoke();
        }

        private static void Invoke(Action[] actions)
        {
            Array.ForEach(actions, Invoke);
        }

        void IDisposable.Dispose()
        {
            Invoke(actions);
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await Task.Run(() => Invoke(actions))
                .ConfigureAwait(false);
        }
    }
}