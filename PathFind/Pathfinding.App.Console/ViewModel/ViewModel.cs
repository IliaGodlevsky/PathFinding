using Pathfinding.App.Console.Menu.Realizations.Attributes;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    internal abstract class ViewModel : IViewModel, IDisposable
    {
        public event Action ViewClosed;

        public virtual void Dispose()
        {
            ViewClosed = null;
        }

        protected virtual void RaiseViewClosed()
        {
            ViewClosed?.Invoke();
        }

        [MenuItem(MenuItemsNames.Exit, int.MaxValue)]
        protected void Exit()
        {
            RaiseViewClosed();
        }
    }
}
