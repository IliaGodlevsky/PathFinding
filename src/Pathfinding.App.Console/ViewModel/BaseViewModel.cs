using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.ViewModel
{
    internal abstract class BaseViewModel : ReactiveObject
    {
        protected async Task ExecuteSafe(Func<Task> action, Action<Exception, string> log)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                log(ex, ex.Message);
            }
        }
    }
}
