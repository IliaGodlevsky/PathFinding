using Pathfinding.App.Console.Delegates;
using Pathfinding.App.Console.Interface;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    internal abstract class ViewModel : IViewModel, IDisposable
    {
        public event Action ViewClosed;

        protected readonly ILog log;

        protected ViewModel(ILog log)
        {
            this.log = log;
        }

        public virtual void Dispose()
        {
            ViewClosed = null;
        }

        protected void RaiseViewClosed()
        {
            ViewClosed?.Invoke();
        }

        protected void ExecuteSafe(Command command)
        {
            try
            {
                command.Invoke();
            }
            catch(Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
