using Pathfinding.App.Console.Model.Menu.Delegates;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    internal abstract class SafeViewModel : ViewModel
    {
        protected readonly ILog log;

        protected SafeViewModel(ILog log)
        {
            this.log = log;
        }

        protected void ExecuteSafe(Command command)
        {
            try
            {
                command.Invoke();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
